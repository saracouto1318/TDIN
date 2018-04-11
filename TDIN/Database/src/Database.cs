using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace Database
{
    public class Database
    {
        public static Database instance;

        private SQLiteConnection _conn;

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

            _conn = new SQLiteConnection("Data Source=" + DB_PATH + ";Version=3;;foreign keys=true;");
            _conn.Open();

            if(justCreated)
            {
                CreateDB();
            }
        }

        private void CreateDB()
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteTransaction transaction = null;
            command.CommandText = File.ReadAllText(SQL_PATH);

            try
            {
                transaction = _conn.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                try
                {
                    File.Delete(SQL_PATH);
                } catch(Exception) { }

                if(transaction != null)
                    transaction.Rollback();
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
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteTransaction transaction = null;

            command.CommandText = 
                "DELETE FROM User; " +
                "DELETE FROM Value; " +
                "DELETE FROM Diginote; " +
                "DELETE FROM Transactions; " +
                "DELETE FROM TransactionDiginote; " +
                "DELETE FROM Session";

            try
            {
                transaction = _conn.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SQLiteException) {
                if(transaction != null)
                    transaction.Rollback();
                return false;
            }
        }

        public static string SafeGetString(SQLiteDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return null;
        }

        public static float SafeGetFloat(SQLiteDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetFloat(colIndex);
            return -1f;
        }

        #region User

        public bool UserExists(string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;

            command.CommandText = "SELECT nickname FROM User WHERE nickname = @nick ";
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                reader = command.ExecuteReader();

                bool exists = reader.Read();
                reader.Close();
                return exists;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if (reader != null)
                    reader.Close();

                return false;
            }
        }

        public bool ValidateUser(string username, string password)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;

            command.CommandText = "SELECT nickname FROM User WHERE nickname = @nick AND password = @pass ";
            command.Parameters.Add(new SQLiteParameter("@nick", username));
            command.Parameters.Add(new SQLiteParameter("@pass", password));

            try
            {
                reader = command.ExecuteReader();

                bool exists = reader.Read();
                reader.Close();
                return exists;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if (reader != null)
                    reader.Close();

                return false;
            }
        }

        public bool InsertUser(string name, string username, string password)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            try
            {
                command.CommandText = "INSERT INTO User(nickname, name, password, balance) VALUES (@nick, @name, @password, 0)";
                command.Parameters.Add(new SQLiteParameter("@nick", username));
                command.Parameters.Add(new SQLiteParameter("@name", name));
                command.Parameters.Add(new SQLiteParameter("@password", password));

                int rowCount = command.ExecuteNonQuery();

                InsertDiginotes(username, 10);

                // If number of affected rows is lower than 1 return false
                return rowCount >= 1;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        public User GetUser(string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;
            User userInfo = new User();

            command.CommandText = 
                "SELECT name, availableDig, totalDig, balance FROM User " +
                "INNER JOIN(" +
                    "SELECT COUNT(*) as availableDig FROM Diginote " +
                        "WHERE owner = @nick AND serialNumber NOT IN ( " + 
                            "SELECT diginoteID FROM TransactionDiginote, Transactions " +
                            "WHERE Transactions.transactionID = TransactionDiginote.transactionID " +
                            "AND ((seller IS NULL AND buyer IS NOT NULL) OR (buyer IS NULL AND seller IS NOT NULL)))) " +
                "INNER JOIN(" +
                    "SELECT COUNT(*) AS totalDig FROM Diginote WHERE owner = @nick) " +
                "WHERE nickname = @nick ";
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    userInfo.username = username;
                    userInfo.name = SafeGetString(reader, 0);
                    userInfo.availableDiginotes = reader.GetInt32(1);
                    userInfo.totalDiginotes = reader.GetInt32(2);
                    userInfo.balance = reader.GetFloat(3);
                }

                reader.Close();
                return userInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(reader != null)
                    reader.Close();

                return userInfo;
            }
        }

        public bool ChangeName(string name, string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            command.CommandText = "UPDATE User SET name=@name WHERE nickname=@nick";
            command.Parameters.Add(new SQLiteParameter("@name", name));
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        public bool ChangeUsername(string newNickname, string oldNickname)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            command.CommandText = "UPDATE User SET nickname=@nickNew WHERE nickname=@nickOld";
            command.Parameters.Add(new SQLiteParameter("@nickNew", newNickname));
            command.Parameters.Add(new SQLiteParameter("@nickOld", oldNickname));
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        public bool ChangePassword(string password, string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            command.CommandText = "UPDATE User SET password=@pass WHERE nickname=@nick";
            command.Parameters.Add(new SQLiteParameter("@pass", password));
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        #endregion

        #region Session

        public bool InsertSession(string username, string session)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            command.CommandText = "INSERT INTO Session(sessionID, nickname) VALUES (@session, @nick)";
            command.Parameters.Add(new SQLiteParameter("@session", session));
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                int rowCount = command.ExecuteNonQuery();
                // If number of affected rows is lower than 1 return false
                return rowCount >= 1;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        public string GetUsername(string session)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;

            command.CommandText = "SELECT nickname FROM Session WHERE sessionID = @session";
            command.Parameters.Add(new SQLiteParameter("@session", session));

            try
            {
                reader = command.ExecuteReader();
                string username = null;

                if (reader.Read())
                {
                    username = SafeGetString(reader, 0);
                }

                reader.Close();
                return username;
            }
            catch (Exception)
            {
                if(reader != null)
                    reader.Close();
            }

            return null;
        }

        public void DeleteSession(string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            command.CommandText = "DELETE FROM Session WHERE nickname=@nick";
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SQLiteException)
            {
            }
        }

        #endregion

        #region Value

        public bool ChangeDiginoteValue(float power)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            command.CommandText = "INSERT INTO Value(power) VALUES (@power)";
            command.Parameters.Add(new SQLiteParameter("@power", power));
            command.Parameters.Add(new SQLiteParameter("@quantity", 0));

            try
            {
                command.ExecuteNonQuery();
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
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;
            float value = 0;

            command.CommandText = "SELECT power FROM Value ORDER BY ID DESC LIMIT 1";
            try
            {
                reader = command.ExecuteReader();

                if (reader.Read())
                    value = reader.GetFloat(0);
                reader.Close();
                Console.WriteLine("diginote value {0}", value);
                return value;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(reader != null)
                    reader.Close();
                return value;
            }
        }

        public Dictionary<float, int> GetQuotes()
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;

            Dictionary<float, int> quotes = new Dictionary<float, int>();

            command.CommandText = "SELECT power, quantity FROM Value";
            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    float quote = reader.GetFloat(0);
                    int quantity = reader.GetInt32(1);
                    if (quotes.ContainsKey(quote))
                    {
                        foreach (var pair in quotes)
                        {
                            if (pair.Key == quote)
                            {
                                quantity += pair.Value;
                                quotes.Remove(pair.Key);
                                break;
                            }
                        }
                    }
                    else
                        quotes.Add(quote, quantity);
                }   
                    
                reader.Close();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(reader != null)
                    reader.Close();
            }

            return quotes;
        }

        public bool IncrementQuantity()
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            command.CommandText = "UPDATE Value SET quantity=quantity+1 WHERE ID IN (SELECT ID FROM Value ORDER BY ID DESC LIMIT 1)";

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        #endregion

        #region Diginote

        public List<int> GetDiginotes(string sessionID)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;
            string username = GetUsername(sessionID);
            List<int> diginotes = new List<int>();

            try
            {
                command.CommandText = "SELECT serialNumber FROM Diginote WHERE owner = @source";
                command.Parameters.Add(new SQLiteParameter("@source", username));
                reader = command.ExecuteReader();

                while (reader.Read())
                    diginotes.Add(reader.GetInt32(0));
                reader.Close();
                return diginotes;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(reader != null)
                    reader.Close();
                return diginotes;
            }
        }
        public List<int> GetAvailableDiginotes(string username, int nDiginotes)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;
            List<int> diginotes = new List<int>();

            try
            {
                command.CommandText = "SELECT serialNumber FROM Diginote " +
                    "WHERE owner = @source AND serialNumber NOT IN ( " +
                    "SELECT diginoteID FROM TransactionDiginote, Transactions " +
                        "WHERE Transactions.transactionID = TransactionDiginote.transactionID " +
                        "AND ((seller IS NULL AND buyer IS NOT NULL) OR (buyer IS NULL AND seller IS NOT NULL))) " +
                    "ORDER BY serialNumber LIMIT @num";
                command.Parameters.Add(new SQLiteParameter("@source", username));
                command.Parameters.Add(new SQLiteParameter("@num", nDiginotes));
                reader = command.ExecuteReader();

                while (reader.Read())
                    diginotes.Add(reader.GetInt32(0));
                reader.Close();
                return diginotes;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(reader != null)
                    reader.Close();
                return diginotes;
            }
        }

        private bool InsertDiginote(string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            command.CommandText = "INSERT INTO Diginote(owner, facialValue) VALUES (@owner, @facialValue)";
            command.Parameters.Add(new SQLiteParameter("@owner", username));
            command.Parameters.Add(new SQLiteParameter("@facialValue", 1));

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        private bool InsertDiginotes(string username, int nDiginotes)
        {
            for (int i = 0; i < nDiginotes; i++)
                InsertDiginote(username);
            return true;
        }

        #endregion

        #region Transaction
        
        public List<Transaction> GetUnfufilledTransactions(int limit, TransactionType type)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                switch (type)
                {
                    case TransactionType.BUY:
                        command.CommandText =
                            "SELECT transactionID, seller, buyer, dateTime, quantity " +
                            "FROM Transactions " +
                            "WHERE buyer IS NULL AND isTransactable = 1 " +
                            "ORDER BY dateTime LIMIT @limit";
                        break;
                    case TransactionType.SELL:
                        command.CommandText =
                            "SELECT transactionID, seller, buyer, dateTime, quantity " +
                            "FROM Transactions " +
                            "WHERE seller IS NULL AND isTransactable = 1 " +
                            "ORDER BY dateTime LIMIT @limit";
                        break;
                    default:
                        return transactions;
                }

                command.Parameters.Add(new SQLiteParameter("@limit", limit));
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Transaction t = new Transaction
                    {
                        ID = reader.GetInt32(0),
                        seller = SafeGetString(reader, 1),
                        buyer = SafeGetString(reader, 2),
                        date = reader.GetDateTime(3),
                        quantity = reader.GetInt32(4)
                    };

                    transactions.Add(t);
                }

                reader.Close();
                return transactions;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(reader != null)
                    reader.Close();
                return transactions;
            }
        }

        public bool CompleteTransaction(Transaction t, int nDiginotes, TransactionType type)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteTransaction dbTransaction = null;

            try
            {
                dbTransaction = _conn.BeginTransaction();
                bool success = UpdateTransaction(t, nDiginotes, type);

                if (!success)
                {
                    Console.WriteLine("Error on update");
                    dbTransaction.Rollback();
                    return false;
                }

                if (type == TransactionType.SELL && !InsertTransactionDiginote(t.ID, GetAvailableDiginotes(t.seller, nDiginotes)))
                {
                    Console.WriteLine("Error on inserting transaction diginotes");
                    dbTransaction.Rollback();
                    return false;
                }

                if (ChangeDiginoteOwner(t.ID, t.buyer) <= 0)
                {
                    Console.WriteLine("Error on change owner");
                    dbTransaction.Rollback();
                    return false;
                }

                float funds = GetValue() * (t.quantity < nDiginotes ? t.quantity : nDiginotes);
                success = AddingFunds(t.seller, funds);
                if (!success)
                {
                    Console.WriteLine("Error on adding funds");
                    dbTransaction.Rollback();
                    return false;
                }

                success = RemovingFunds(t.buyer, funds);
                if (!success)
                {
                    Console.WriteLine("Error on removing funds");
                    dbTransaction.Rollback();
                    return false;
                }

                dbTransaction.Commit();

                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(dbTransaction != null)
                    dbTransaction.Rollback();
                return false;
            }
        }

        public long InsertTransaction(Transaction t, TransactionType type)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteTransaction dbTransaction = null;

            try
            {
                dbTransaction = _conn.BeginTransaction();

                float value = GetValue() * t.quantity;
                if ((type.Equals(TransactionType.BUY) && GetUser(t.buyer).balance >= value) ||
                    (type.Equals(TransactionType.SELL) && GetAvailableDiginotes(t.seller, t.quantity).Count >= t.quantity))
                {
                    bool success = InsertTransactionDirect(t);
                    if (success)
                    {
                        long rowID = _conn.LastInsertRowId;

                        dbTransaction.Commit();
                        return rowID;
                    }
                }

                dbTransaction.Rollback();
                return -1;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(dbTransaction != null)
                    dbTransaction.Rollback();
                return -1;
            }
        }

        public List<Transaction> GetTransactions(TransactionType type, bool open, string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;

            List<Transaction> transactions = new List<Transaction>();
            if (type == TransactionType.ALL)
                command.CommandText = "SELECT * FROM Transactions WHERE buyer = @nick OR seller = @nick";
            else if (type == TransactionType.BUY && open)
                command.CommandText = "SELECT * FROM Transactions WHERE buyer = @nick AND seller IS NULL";
            else if (type == TransactionType.BUY && !open)
                command.CommandText = "SELECT * FROM Transactions WHERE buyer = @nick AND seller IS NOT NULL";
            else if (type == TransactionType.SELL && open)
                command.CommandText = "SELECT * FROM Transactions WHERE seller = @nick AND buyer IS NULL";
            else if (type == TransactionType.SELL && !open)
                command.CommandText = "SELECT * FROM Transactions WHERE seller = @nick AND buyer IS NOT NULL";
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Transaction info = new Transaction
                    {
                        ID = reader.GetInt32(0),
                        seller = SafeGetString(reader, 1),
                        buyer = SafeGetString(reader, 2),
                        date = reader.GetDateTime(3),
                        quantity = reader.GetInt32(4),
                        quotation = SafeGetFloat(reader, 6)
                    };

                    transactions.Add(info);
                }

                reader.Close();
                return transactions;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(reader != null)
                    reader.Close();
                return null;
            }
        }
        
        public List<Transaction> GetOtherTransactions(TransactionType type, bool open, string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            SQLiteDataReader reader = null;

            List<Transaction> transactions = new List<Transaction>();
            if (type == TransactionType.ALL)
                command.CommandText = "SELECT * FROM Transactions WHERE buyer != @nick AND seller != @nick LIMIT Transaction.quantity";
            else if (type == TransactionType.BUY && open)
                command.CommandText = "SELECT * FROM Transactions WHERE buyer != @nick AND seller IS NULL";
            else if (type == TransactionType.BUY && !open)
                command.CommandText = "SELECT * FROM Transactions WHERE buyer != @nick AND seller IS NOT NULL AND seller != @nick";
            else if (type == TransactionType.SELL && open)
                command.CommandText = "SELECT * FROM Transactions WHERE seller != @nick AND buyer IS NULL";
            else if (type == TransactionType.SELL && !open)
                command.CommandText = "SELECT transactionID, seller, buyer FROM Transaction WHERE seller != @nick AND buyer IS NOT NULL AND buyer != @nick";
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Transaction info = new Transaction
                    {
                        ID = reader.GetInt32(0),
                        seller = SafeGetString(reader, 1),
                        buyer = SafeGetString(reader, 2),
                        date = reader.GetDateTime(3),
                        quantity = reader.GetInt32(4)
                    };

                    transactions.Add(info);
                }

                reader.Close();
                return transactions;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                if(reader != null)
                    reader.Close();
                return null;
            }
        }

        public void SetActiveTransactions(bool active)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            try
            {
                command.CommandText = 
                    "UPDATE Transactions SET isTransactable=@active " +
                    "WHERE ((seller IS NULL AND buyer IS NOT NULL) OR (buyer IS NULL AND seller IS NOT NULL))";
                command.Parameters.Add(new SQLiteParameter("@active", active));
                command.ExecuteNonQuery();
            } catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void SetActiveMyTransaction(bool active, string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            try
            {
                command.CommandText =
                    "UPDATE Transactions SET isTransactable=@active " +
                    "WHERE (buyer = @nick AND seller IS NULL) OR (seller = @nick AND buyer IS NULL)";
                command.Parameters.Add(new SQLiteParameter("@active", active ? 1 : 0));
                command.Parameters.Add(new SQLiteParameter("@nick", username));
                int rows = command.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public bool DeleteMyInactiveTransactions(string username)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            try
            {
               command.CommandText =
                    "DELETE FROM Transactions WHERE isTransactable=0 AND (buyer = @nick OR seller = @nick)";
               command.Parameters.Add(new SQLiteParameter("@nick", username));
               command.ExecuteNonQuery();
                
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        private bool InsertTransactionDirect(Transaction transaction)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            try
            {
                command.CommandText = 
                    "INSERT INTO Transactions(seller, buyer, dateTime, quantity, isTransactable) VALUES (@seller, @buyer, @time, @quantity, 1)";
                command.Parameters.Add(new SQLiteParameter("@seller", transaction.seller));
                command.Parameters.Add(new SQLiteParameter("@buyer", transaction.buyer));
                command.Parameters.Add(new SQLiteParameter("@time", transaction.date));
                command.Parameters.Add(new SQLiteParameter("@quantity", transaction.quantity));

                command.ExecuteNonQuery();

                transaction.ID = (int)_conn.LastInsertRowId;

                if(transaction.buyer == null)
                    return InsertTransactionDiginote(transaction.ID, GetAvailableDiginotes(transaction.seller, transaction.quantity));
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        private bool InsertTransactionDiginote(int transactionID, List<int> diginotes)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            try
            {
                command.CommandText = "INSERT INTO TransactionDiginote(transactionID, diginoteID) VALUES (@transactionID, @diginoteID)";
                foreach(int diginote in diginotes)
                {
                    command.Parameters.Add(new SQLiteParameter("@transactionID", transactionID));
                    command.Parameters.Add(new SQLiteParameter("@diginoteID", diginote));

                    command.ExecuteNonQuery();
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

        private bool UpdateTransaction(Transaction transaction, int nDiginotes, TransactionType type)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);

            try
            {
                bool isInsertTransaction = false;
                float quotation = GetValue();
                string sqlCommand = "UPDATE Transactions";

                switch (type)
                {
                    case TransactionType.BUY:
                        Console.WriteLine("BUYER:" + transaction.buyer);
                        sqlCommand += " SET buyer=@buyer";
                        command.Parameters.Add(new SQLiteParameter("@buyer", transaction.buyer));
                        break;
                    case TransactionType.SELL:
                        Console.WriteLine("SELLER:" + transaction.seller);
                        sqlCommand += " SET seller=@seller";
                        command.Parameters.Add(new SQLiteParameter("@seller", transaction.seller));
                        break;
                    default:
                        return false;
                }

                if(nDiginotes < transaction.quantity)
                {
                    Console.WriteLine("QUANTITY:" + nDiginotes);
                    sqlCommand += ", quantity=@quantity";
                    command.Parameters.Add(new SQLiteParameter("@quantity", nDiginotes));

                    // Insert new transaction
                    isInsertTransaction = true;
                }

                sqlCommand += ", quotation=@quotation WHERE transactionID = @orderID";

                command.CommandText = sqlCommand;
                command.Parameters.Add(new SQLiteParameter("@quotation", quotation));
                command.Parameters.Add(new SQLiteParameter("@orderID", transaction.ID));
                Console.WriteLine("ID:" + transaction.ID);
                int rows = command.ExecuteNonQuery();

                if (isInsertTransaction)
                {
                    Transaction t = new Transaction
                    {
                        quantity = transaction.quantity - nDiginotes,
                        date = transaction.date
                    };

                    switch (type)
                    {
                        case TransactionType.BUY:
                            t.seller = transaction.seller;
                            break;
                        case TransactionType.SELL:
                            t.buyer = transaction.buyer;
                            break;
                        default:
                            return false;
                    }

                    InsertTransactionDirect(t);
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
            SQLiteCommand command = new SQLiteCommand(_conn);

            try
            {
                command.CommandText = "UPDATE Diginote SET owner=@owner WHERE serialNumber IN (SELECT diginoteID FROM TransactionDiginote WHERE transactionID = @orderID)";
                command.Parameters.Add(new SQLiteParameter("@owner", username));
                command.Parameters.Add(new SQLiteParameter("@orderID", orderID));
                int changedRows = command.ExecuteNonQuery();

                return changedRows;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return 0;
            }
        }

        #endregion

        #region Funds

        public bool AddingFunds(string username, double funds)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            command.CommandText = "UPDATE User SET balance=balance + @funds WHERE nickname=@nick";
            command.Parameters.Add(new SQLiteParameter("@funds", funds));
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        private bool RemovingFunds(string username, double funds)
        {
            SQLiteCommand command = new SQLiteCommand(_conn);
            command.CommandText = "UPDATE User SET balance=balance - @funds WHERE nickname=@nick";
            command.Parameters.Add(new SQLiteParameter("@funds", funds));
            command.Parameters.Add(new SQLiteParameter("@nick", username));

            try
            {
                command.ExecuteNonQuery();
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
