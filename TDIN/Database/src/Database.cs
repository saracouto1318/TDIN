using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Database
{
    public enum TransactionType { ALL, SELL, BUY };

    public class Database
    {
        public static Database instance;

        private SQLiteConnection _conn;
        private SQLiteCommand _command;
        private SQLiteTransaction _transaction;
        private SQLiteDataReader _reader;

        public const string DB_PATH = "database.sqlite";
        public const string SQL_PATH = "database.sql";
        public const float DEFAULT_VALUE = 1.0f;
        public const float INC_DEC_VALUE = 0.1f;

        private Database()
        {
            StartDB();
        }

        public static Database Initialize()
        {
            if (instance == null)
            {
                instance = new Database();
                return instance;
            }
            return null;
        }

        private void StartDB()
        {
            bool justCreated = false;
            if (!File.Exists(DB_PATH))
            {
                justCreated = true;
                SQLiteConnection.CreateFile(DB_PATH);
            }
            
            _conn = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;");

            _command = new SQLiteCommand(_conn);
            _conn.Open();

            if(justCreated)
            {
                CreateDB();
            }
        }

        private void CreateDB()
        {
            _command.CommandText = File.ReadAllText(SQL_PATH);

            try
            {
                _transaction = _conn.BeginTransaction();
                _command.ExecuteNonQuery();
                _transaction.Commit();
            }
            catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                _transaction.Rollback();
            }

            ChangeDiginoteValue(DEFAULT_VALUE);
        }

        private void ConnectDB()
        {
            _conn.Open();
        }

        private void CloseDB()
        {
            _conn.Close();
        }
        
        public bool DeleteDB()
        {
            _command.CommandText = "DELETE FROM User; DELETE FROM Value; DELETE FROM Diginote; DELETE FROM Transactions; DELETE FROM TransactionDiginote; DELETE FROM Session";

            try
            {
                _transaction = _conn.BeginTransaction();
                _command.ExecuteNonQuery();
                _transaction.Commit();
                return true;
            }
            catch (SQLiteException) {
                _transaction.Rollback();
                return false;
            }
        }

        #region User

        public bool UserExists(string username)
        {
            _command.CommandText = "SELECT nickname FROM User WHERE nickname = @nick ";
            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                _reader = _command.ExecuteReader();

                bool exists = _reader.Read();
                _reader.Close();
                return exists;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool CheckUser(string username, string password)
        {
            _command.CommandText = "SELECT nickname FROM User WHERE nickname = @nick AND password = @pass ";
            _command.Parameters.Add(new SQLiteParameter("@nick", username));
            _command.Parameters.Add(new SQLiteParameter("@pass", password));

            try
            {
                _reader = _command.ExecuteReader();

                bool exists = _reader.Read();
                _reader.Close();
                return exists;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool InsertUser(string name, string username, string password)
        {
            _command.CommandText = "INSERT INTO User(nickname, name, password, balance) VALUES (@nick, @name, @password, 0)";
            _command.Parameters.Add(new SQLiteParameter("@nick", username));
            _command.Parameters.Add(new SQLiteParameter("@name", name));
            _command.Parameters.Add(new SQLiteParameter("@password", password));

            try
            {
                int rowCount = _command.ExecuteNonQuery();
                // If number of affected rows is lower than 1 return false
                return rowCount >= 1;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public User GetUserInfo(string username)
        {
            User userInfo = new User();

            _command.CommandText = "SELECT name, availableDig, totalDig FROM User " +
                "INNER JOIN(SELECT COUNT(*) as availableDig FROM Diginote WHERE owner = 'vsp' AND serialNumber NOT IN(SELECT diginoteID FROM TransactionDiginote)) " +
                "INNER JOIN(SELECT COUNT(*) AS totalDig FROM Diginote WHERE owner = 'vsp') " +
                "WHERE nickname = @nick ";

            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                _reader = _command.ExecuteReader();

                if (_reader.Read())
                {
                    userInfo.username = username;
                    userInfo.name = _reader.GetString(0);
                    userInfo.availableDiginotes = _reader.GetInt32(1);
                    userInfo.totalDiginotes = _reader.GetInt32(2);
                    userInfo.balance = _reader.GetFloat(3);
                }

                _reader.Close();
                return userInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                _reader.Close();
                return userInfo;
            }
        }

        public bool ChangeName(string name, string username)
        {
            _command.CommandText = "UPDATE User SET name=@name WHERE nickname=@nick";
            _command.Parameters.Add(new SQLiteParameter("@name", name));
            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                _command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public bool ChangeUsername(string newNickname, string oldNickname)
        {
            _command.CommandText = "UPDATE User SET nickname=@nickNew WHERE nickname=@nickOld";
            _command.Parameters.Add(new SQLiteParameter("@nickNew", newNickname));
            _command.Parameters.Add(new SQLiteParameter("@nickOld", oldNickname));
            try
            {
                _command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public bool ChangePassword(string password, string username)
        {
            _command.CommandText = "UPDATE User SET password=@pass WHERE nickname=@nick";
            _command.Parameters.Add(new SQLiteParameter("@pass", password));
            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                _command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public bool AddingFunds(string username, double funds)
        {
            _command.CommandText = "UPDATE User SET balance=balance + @funds WHERE nickname=@nick";
            _command.Parameters.Add(new SQLiteParameter("@pass", funds));
            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                _command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public bool RemovingFunds(string username, double funds)
        {
            _command.CommandText = "UPDATE User SET balance=balance - @funds WHERE nickname=@nick";
            _command.Parameters.Add(new SQLiteParameter("@pass", funds));
            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                _command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        #endregion

        #region Session

        public bool InsertSession(string username, string session)
        {
            _command.CommandText = "INSERT INTO Session(sessionID, nickname) VALUES (@session, @nick)";
            _command.Parameters.Add(new SQLiteParameter("@session", session));
            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                int rowCount = _command.ExecuteNonQuery();
                // If number of affected rows is lower than 1 return false
                return rowCount >= 1;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public string GetUsernameBySession(string session)
        {
            _command.CommandText = "SELECT nickname FROM Session WHERE sessionID = @session";
            _command.Parameters.Add(new SQLiteParameter("@session", session));

            try
            {
                _reader = _command.ExecuteReader();
                string username = null;

                if (_reader.Read())
                {
                    username = _reader.GetString(0);
                }

                _reader.Close();
                return username;
            }
            catch (Exception)
            {
                _reader.Close();
            }

            return null;
        }

        public void DeleteSession(string username)
        {
            _command.CommandText = "DELETE FROM Session WHERE nickname=@nick";
            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (SQLiteException)
            {
            }
        }

        #endregion

        #region Diginote

        public bool InsertDiginote(string username)
        {
            _command.CommandText = "INSERT INTO Diginote(owner, facialValue) VALUES (@owner, @facialValue)";
            _command.Parameters.Add(new SQLiteParameter("@owner", username));
            _command.Parameters.Add(new SQLiteParameter("@facialValue", 1));

            try
            {
                _command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public bool InsertDiginotes(string username, int nDiginotes)
        {
            for (int i = 0; i < nDiginotes; i++)
                InsertDiginote(username);
            return true;
        }

        public bool ChangeDiginoteValue(float power)
        {
            _command.CommandText = "INSERT INTO Value(power) VALUES (@power)";
            _command.Parameters.Add(new SQLiteParameter("@power", power));
            _command.Parameters.Add(new SQLiteParameter("@quantity", 0));

            try
            {
                _command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public float GetValue()
        {
            float value = 0;
            _command.CommandText = "SELECT power FROM Value ORDER BY ID DESC LIMIT 1";
            try
            {
                _reader = _command.ExecuteReader();

                if (_reader.Read())
                    value = _reader.GetFloat(0);
                _reader.Close();
                Console.WriteLine("diginite value {0}", value);
                return value;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                _reader.Close();
                return value;
            }
        }

        #endregion

        #region Transaction

        public List<int> GetAvailableDiginotes(string username, int nDiginotes)
        {
            List<int> diginotes = new List<int>();

            try
            {
                _command.CommandText = "SELECT serialNumber FROM Diginote WHERE nickname = @source AND serialNumber NOT IN (SELECT diginoteID FROM TransactionDiginote) ORDER BY serialNumber LIMIT @num";
                _command.Parameters.Add(new SQLiteParameter("@source", username));
                _command.Parameters.Add(new SQLiteParameter("@num", nDiginotes));
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                    diginotes.Add(_reader.GetInt32(0));
                _reader.Close();
                return diginotes;
            }
            catch(SQLiteException)
            {
                _reader.Close();
                return diginotes;
            }
        }

        public bool InsertTransaction(int nDiginotes, string buyer, string seller, TransactionType type, DateTime time)
        {
            List<Transaction> transactions = GetUnfufilledTransactions(nDiginotes, type);
            foreach(Transaction t in transactions)
            {

            }

            /*float value = GetValue() * nDiginotes;
            try
            {
                if((type.Equals(TransactionType.BUY) && GetUserInfo(buyer).balance >= value) || (type.Equals(TransactionType.SELL) && GetAvailableDiginotes(seller, nDiginotes).Count >= nDiginotes))
                {
                    _command.CommandText = "INSERT INTO Transactions(seller, buyer, price, dateTime, quantity) VALUES (@seller, @buyer, @price, @time, @quantity)";
                    _command.Parameters.Add(new SQLiteParameter("@seller", seller));
                    _command.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                    _command.Parameters.Add(new SQLiteParameter("@price", value));
                    _command.Parameters.Add(new SQLiteParameter("@time", time));
                    _command.Parameters.Add(new SQLiteParameter("@quantity", nDiginotes));
                    
                    _command.ExecuteNonQuery();
                }
                
                return CheckTransactions(nDiginotes, seller, buyer, type);
            }
            catch (SQLiteException)
            {
                return false;
            }*/
            return false;
        }

        public List<Transaction> GetUnfufilledTransactions(int limit, TransactionType type)
        {
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                switch(type)
                {
                    case TransactionType.BUY:
                        _command.CommandText = "SELECT * FROM Transactions WHERE buyer IS NULL ORDER BY dateTime LIMIT @limit";
                        break;
                    case TransactionType.SELL:
                        _command.CommandText = "SELECT * FROM Transactions WHERE seller IS NULL ORDER BY dateTime LIMIT @limit";
                        break;
                    default:
                        return transactions;
                }

                _command.Parameters.Add(new SQLiteParameter("@limit", limit));
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    Transaction t = new Transaction
                    {
                        ID = _reader.GetInt32(0),
                        quantity = _reader.GetInt32(5)
                    };
                    transactions.Add(t);
                }

                _reader.Close();
                return transactions;
            } catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                _reader.Close();
                return transactions;
            }
        }

        public bool CheckTransactions(int numDiginotes, string seller, string buyer, TransactionType type)
        {
            /*try
            {
                if (type.Equals(TransactionType.SELL)) {
                    _command.CommandText = "SELECT * FROM Transactions WHERE seller IS NULL AND quantity >= @num AND buyer <> @seller ORDER BY transactionID LIMIT 1";
                    _command.Parameters.Add(new SQLiteParameter("@seller", seller));
                }
                    
                else if (type.Equals(TransactionType.BUY))
                {
                    _command.CommandText = "SELECT * FROM Transactions WHERE buyer IS NULL AND quantity >= @num AND seller <> @buyer ORDER BY transactionID LIMIT 1";
                    _command.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                }

                _command.Parameters.Add(new SQLiteParameter("@num", numDiginotes));
                _reader = _command.ExecuteReader();

                if (_reader.Read())
                {
                    int newQuantity = _reader.GetInt32(5) - numDiginotes;
                    int compOrderID = _reader.GetInt32(0);
                    string transactionBuyer = _reader.GetString(2);
                    string transactionSeller = _reader.GetString(1);

                    if (newQuantity != 0)
                    {                        
                        //change quotation
                        /*float newQuote;
                        if (type.Equals(TransactionType.SELL))
                        {
                            newQuote = GetValue() - INC_DEC_VALUE;
                            ChangeDiginoteValue(newQuote);
                        }
                        else if (type.Equals(TransactionType.BUY))
                        {
                            newQuote = GetValue() + INC_DEC_VALUE;
                            ChangeDiginoteValue(newQuote);
                        *//*
                    }

                    if (type.Equals(TransactionType.SELL))
                        return CompleteTransaction(type, seller, transactionBuyer, numDiginotes, transactionID, compOrderID);
                    else if(type.Equals(TransactionType.BUY))
                        return CompleteTransaction(type, transactionSeller, buyer, numDiginotes, transactionID, compOrderID);
                }

                return false;
            }
            catch (SQLiteException e)
            {
                _transaction.Rollback();
                return false;
            }*/
            return false;
        }

        public bool CompleteTransaction(int orderID, string buyer, string seller, float price, TransactionType type)
        {
            try
            {
                _transaction = _conn.BeginTransaction();
                bool success = false;

                switch (type)
                {
                    case TransactionType.BUY:
                        success = UpdateTransaction(orderID, buyer, type);
                        break;
                    case TransactionType.SELL:
                        success = UpdateTransaction(orderID, seller, type);
                        break;
                    default:
                        return false;
                }

                if(!success)
                {
                    _transaction.Rollback();
                    return false;
                }

                if(ChangeDiginoteOwner(orderID, buyer) <= 0)
                {
                    _transaction.Rollback();
                    return false;
                }

                IncrementQuantity();
                
                success = AddingFunds(seller, price);
                if (!success)
                {
                    _transaction.Rollback();
                    return false;
                }

                success = RemovingFunds(buyer, price);
                if (!success)
                {
                    _transaction.Rollback();
                    return false;
                }

                _transaction.Commit();

                return true;
            }
            catch (SQLiteException)
            {
                _transaction.Rollback();
                return false;
            }
        }

        private bool UpdateTransaction(int orderID, string username, TransactionType type)
        {
            try
            {
                _transaction = _conn.BeginTransaction();

                switch (type)
                {
                    case TransactionType.BUY:
                        _command.CommandText =
                            "UPDATE Transactions SET buyer=@buyer WHERE transactionID = @orderID";
                        _command.Parameters.Add(new SQLiteParameter("@buyer", username));
                        break;
                    case TransactionType.SELL:
                        _command.CommandText =
                            "UPDATE Transactions SET seller=@seller WHERE transactionID = @orderID";
                        _command.Parameters.Add(new SQLiteParameter("@seller", username));
                        break;
                    default:
                        return false;
                }

                return true;
            }
            catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        private int ChangeDiginoteOwner(int orderID, string username)
        {
            try
            {
                _command.CommandText = "UPDATE Diginote SET nickname=@owner WHERE serialNumber = (SELECT diginoteID FROM TransactionDiginote WHERE transactionID = @orderID)";
                _command.Parameters.Add(new SQLiteParameter("@owner", username));
                _command.Parameters.Add(new SQLiteParameter("@orderID", orderID));
                int changedRows = _command.ExecuteNonQuery();

                return changedRows;
            } catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return 0;
            }
        }

        private bool IncrementQuantity()
        {
            try
            {
                _command.CommandText = "UPDATE Value SET quantity=quantity+1 WHERE ID=@ID";
                _command.Parameters.Add(new SQLiteParameter("@ID", _reader.GetInt32(1)));

                _command.ExecuteNonQuery();

                return true;
            } catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        public List<Transaction> GetTransactions(TransactionType type, bool open, string username)
        {
            List<Transaction> transactions = new List<Transaction>();
            if (type == TransactionType.ALL)
                _command.CommandText = "SELECT * FROM Transactions WHERE buyer = @nick OR seller = @nick LIMIT Transaction.quantity";
            else if (type == TransactionType.BUY && open)
                _command.CommandText = "SELECT * FROM Transactions WHERE buyer = @nick AND seller IS NULL";
            else if (type == TransactionType.BUY && !open)
                _command.CommandText = "SELECT * FROM Transactions WHERE buyer = @nick AND seller IS NOT NULL";
            else if (type == TransactionType.SELL && open)
                _command.CommandText = "SELECT * FROM Transactions WHERE seller = @nick AND buyer IS NULL";
            else if (type == TransactionType.SELL && !open)
                _command.CommandText = "SELECT transactionID, seller, buyer FROM Transaction WHERE seller = @nick AND buyer IS NOT NULL";
            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                _reader = _command.ExecuteReader();

                Transaction info = new Transaction();

                while (_reader.Read())
                {
                    info.ID = _reader.GetInt32(0);
                    info.buyer = _reader.GetString(2);
                    info.seller = _reader.GetString(1);
                    info.value = _reader.GetFloat(3);
                    info.date = _reader.GetDateTime(4);
                    info.quantity = _reader.GetInt32(5);
                    
                    transactions.Add(info);
                }

                _reader.Close();
                return transactions;
            }
            catch (SQLiteException)
            {
                _reader.Close();
                return null;
            }
        }
        
        public List<Transaction> GetOtherTransactions(TransactionType type, bool open, string username)
        {
            List<Transaction> transactions = new List<Transaction>();
            if (type == TransactionType.ALL)
                _command.CommandText = "SELECT * FROM Transactions WHERE buyer != @nick AND seller != @nick LIMIT Transaction.quantity";
            else if (type == TransactionType.BUY && open)
                _command.CommandText = "SELECT * FROM Transactions WHERE buyer != @nick AND seller IS NULL";
            else if (type == TransactionType.BUY && !open)
                _command.CommandText = "SELECT * FROM Transactions WHERE buyer != @nick AND seller IS NOT NULL AND seller != @nick";
            else if (type == TransactionType.SELL && open)
                _command.CommandText = "SELECT * FROM Transactions WHERE seller != @nick AND buyer IS NULL";
            else if (type == TransactionType.SELL && !open)
                _command.CommandText = "SELECT transactionID, seller, buyer FROM Transaction WHERE seller != @nick AND buyer IS NOT NULL AND buyer != @nick";
            _command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                _reader = _command.ExecuteReader();

                Transaction info = new Transaction();

                while (_reader.Read())
                {
                    info.ID = _reader.GetInt32(0);
                    info.buyer = _reader.GetString(2);
                    info.seller = _reader.GetString(1);
                    info.value = _reader.GetFloat(3);
                    info.date = _reader.GetDateTime(4);
                    info.quantity = _reader.GetInt32(5);

                    transactions.Add(info);
                }

                _reader.Close();
                return transactions;
            }
            catch (SQLiteException)
            {
                _reader.Close();
                return null;
            }
        }

        public bool DeleteTransactions(int transactionID)
        {
            try
            {
               _command.CommandText += "DELETE FROM TransactionDiginote WHERE transactionID=@ID; DELETE FROM Transactions WHERE transactionID=@ID";
               _command.Parameters.Add(new SQLiteParameter("@ID", transactionID));
               _command.ExecuteNonQuery();
                
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        #endregion
    }
}
