using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Database
{
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

        public bool UserExists(string nickname)
        {
            _command.CommandText = "SELECT nickname FROM User WHERE nickname = @nick ";
            _command.Parameters.Add(new SQLiteParameter("@nick", nickname));

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

        public bool CheckUser(string nickname, string password)
        {
            _command.CommandText = "SELECT nickname FROM User WHERE nickname = @nick AND password = @pass ";
            _command.Parameters.Add(new SQLiteParameter("@nick", nickname));
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

        public bool InsertUser(string name, string nickname, string password)
        {
            _command.CommandText = "INSERT INTO User(nickname, name, password) VALUES (@nick, @name, @password)";
            _command.Parameters.Add(new SQLiteParameter("@nick", nickname));
            _command.Parameters.Add(new SQLiteParameter("@name", name));
            _command.Parameters.Add(new SQLiteParameter("@password", password));

            try
            {
                _transaction = _conn.BeginTransaction();
                int rowCount = _command.ExecuteNonQuery();
                _transaction.Commit();
                // If number of affected rows is lower than 1 return false
                return rowCount >= 1;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                _transaction.Rollback();
                return false;
            }
        }

        public User GetUserInfo(string nickname)
        {
            User userInfo = new User();

            _command.CommandText = "SELECT name " +
                "FROM User " +
                "WHERE nickname = @nick ";
            _command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                _reader = _command.ExecuteReader();

                if (_reader.Read())
                {
                    userInfo.username = nickname;
                    userInfo.name = _reader.GetString(0);
                }

                _reader.Close();
                return userInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return userInfo;
            }
        }

        public bool ChangeName(string name, string nickname)
        {
            _command.CommandText = "UPDATE User SET name=@name WHERE nickname=@nick";
            _command.Parameters.Add(new SQLiteParameter("@name", name));
            _command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                _transaction = _conn.BeginTransaction();
                _command.ExecuteNonQuery();
                _transaction.Commit();
                return true;
            }
            catch (SQLiteException)
            {
                _transaction.Rollback();
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
                _transaction = _conn.BeginTransaction();
                _command.ExecuteNonQuery();
                _transaction.Commit();
                return true;
            }
            catch (SQLiteException)
            {
                _transaction.Rollback();
                return false;
            }
        }

        public bool ChangePassword(string password, string nickname)
        {
            _command.CommandText = "UPDATE User SET password=@pass WHERE nickname=@nick";
            _command.Parameters.Add(new SQLiteParameter("@pass", password));
            _command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                _transaction = _conn.BeginTransaction();
                _command.ExecuteNonQuery();
                _transaction.Commit();
                return true;
            }
            catch (SQLiteException)
            {
                _transaction.Rollback();
                return false;
            }
        }

        #endregion

        #region Session

        public bool InsertSession(string nickname, string session)
        {
            _command.CommandText = "INSERT INTO Session(sessionID, nickname) VALUES (@session, @nick)";
            _command.Parameters.Add(new SQLiteParameter("@session", session));
            _command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                _transaction = _conn.BeginTransaction();
                int rowCount = _command.ExecuteNonQuery();
                _transaction.Commit();
                // If number of affected rows is lower than 1 return false
                return rowCount >= 1;
            }
            catch (SQLiteException)
            {
                _transaction.Rollback();
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

        public void DeleteSession(string nickname)
        {
            _command.CommandText = "DELETE FROM Session WHERE nickname=@nick";
            _command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                _transaction = _conn.BeginTransaction();
                _command.ExecuteNonQuery();
                _transaction.Commit();
            }
            catch (SQLiteException)
            {
            }
        }

        #endregion

        #region Diginote

        public bool InsertDiginote(string nickname)
        {
            _command.CommandText = "INSERT INTO Diginote(owner, facialValue) VALUES (@owner, @facialValue)";
            _command.Parameters.Add(new SQLiteParameter("@owner", nickname));
            _command.Parameters.Add(new SQLiteParameter("@facialValue", 1));

            try
            {
                _transaction = _conn.BeginTransaction();
                _command.ExecuteNonQuery();
                _transaction.Commit();
                return true;
            }
            catch (SQLiteException)
            {
                _transaction.Rollback();
                return false;
            }
        }

        public bool InsertDiginotes(string nickname, int nDiginotes)
        {
            for (int i = 0; i < nDiginotes; i++)
                InsertDiginote(nickname);
            return true;
        }

        public bool ChangeDiginoteValue(float power)
        {
            _command.CommandText = "INSERT INTO Value(power) VALUES (@power)";
            _command.Parameters.Add(new SQLiteParameter("@power", power));
            //command.Parameters.Add(new SQLiteParameter("@quantity", 0));

            try
            {
                _transaction = _conn.BeginTransaction();
                _command.ExecuteNonQuery();
                _transaction.Commit();
                return true;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                _transaction.Rollback();
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

        public List<int> CheckDiginotes(string username, int nDiginotes)
        {
            List<int> diginotes = new List<int>();

            try
            {
                _command.CommandText = "SELECT serialNumber FROM Diginote WHERE nickname = @source AND serialNumber NOT IN (SELECT diginoteID FROM TransactionDiginote) ORDER BY serialNumber LIMIT @num";
                _command.Parameters.Add(new SQLiteParameter("@source", username));
                _command.Parameters.Add(new SQLiteParameter("@num", nDiginotes));
                _reader = _command.ExecuteReader();

                if (_reader.Read())
                    while (_reader.Read())
                        diginotes.Add(_reader.GetInt32(0));
                
                return diginotes;
            }
            catch(SQLiteException)
            {
                _transaction.Rollback();
                return diginotes;
            }
        }

        public bool InsertTransaction(int nDiginotes, string buyer, string seller)
        {
            float value = GetValue() * (float) nDiginotes;
            try
            {
                _command.CommandText = "INSERT INTO Transactions(seller, buyer, price, dateTime, quantity) VALUES (@seller, @buyer, @price, @time, @quantity)";
                _command.Parameters.Add(new SQLiteParameter("@seller", buyer));
                _command.Parameters.Add(new SQLiteParameter("@buyer", seller));
                _command.Parameters.Add(new SQLiteParameter("@price", value));

                DateTime localDate = DateTime.Now;
                _command.Parameters.Add(new SQLiteParameter("@time", localDate));
                _command.Parameters.Add(new SQLiteParameter("@quantity", nDiginotes));

                _transaction = _conn.BeginTransaction();
                _command.ExecuteNonQuery();
                _transaction.Commit();

                if(seller != null)
                {
                    List<int> diginotes = CheckDiginotes(seller, nDiginotes);
                    _command.CommandText = "SELECT transactionID FROM Transactions WHERE diginoteID = @serial";
                    _command.Parameters.Add(new SQLiteParameter("@serial", diginotes[0]));

                    _reader = _command.ExecuteReader();

                    int ID = 0;
                    if (_reader.Read())
                        ID += _reader.GetInt32(0);

                    foreach (int diginote in diginotes)
                    {
                        _transaction = _conn.BeginTransaction();
                        _command.CommandText = "INSERT INTO TransactionDiginote(transactionID, diginoteID) VALUES (@transID, @digiID)";
                        _command.Parameters.Add(new SQLiteParameter("@transID", ID));
                        _command.Parameters.Add(new SQLiteParameter("@digiID", diginote));
                        _command.ExecuteNonQuery();
                        _transaction.Commit();
                    }
                }

                return true;
            }
            catch (SQLiteException)
            {
                _transaction.Rollback();
                return false;
            }
        }

        public bool CompleteTransaction(string seller, string buyer, int nDiginotes, int transactionID)
        {
            try
            {
                List<int> diginotes = CheckDiginotes(seller, nDiginotes);

                foreach (int diginote in diginotes)
                {
                    _transaction = _conn.BeginTransaction();
                    _command.CommandText = "UPDATE Diginote SET nickname=@dest WHERE serialNumber=@serial";
                    _command.Parameters.Add(new SQLiteParameter("@dest", buyer));
                    _command.Parameters.Add(new SQLiteParameter("@serial", diginote));
                    _command.ExecuteNonQuery();

                    _command.CommandText = "UPDATE Transactions SET buyer=@buyer, seller=@seller, price=@price WHERE Transactions.transactionID = (SELECT TransactionDiginote.transactionID FROM TransactionDiginote WHERE diginoteID=@serial)";
                    _command.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                    _command.Parameters.Add(new SQLiteParameter("@seller", seller));
                    _command.Parameters.Add(new SQLiteParameter("@price", nDiginotes * this.GetValue()));
                    _command.Parameters.Add(new SQLiteParameter("@serial", diginote));
                    _command.ExecuteNonQuery();
                }

                _transaction.Commit();

                _command.CommandText = "SELECT quantity, ID FROM Value ORDER BY DESC LIMIT 1";
                _reader = _command.ExecuteReader();

                int currentQuantity = 0;

                if (_reader.Read())
                    currentQuantity += _reader.GetInt32(0);

                _command.CommandText = "UPDATE Value SET quantity=@quantity WHERE ID=@ID";
                _command.Parameters.Add(new SQLiteParameter("@quantity", currentQuantity + 1));
                _command.Parameters.Add(new SQLiteParameter("@ID", _reader.GetInt32(1)));
                _reader.Close();

                _command.ExecuteNonQuery();

                return true;
            }
            catch (SQLiteException)
            {
                _reader.Close();
                _transaction.Rollback();
                return false;
            }
        }

        public List<Transaction> GetTransactions(string type, bool open, string nickname)
        {
            List<Transaction> transactions = new List<Transaction>();
            if (type == "all")
                _command.CommandText = "SELECT * FROM Transactions WHERE buyer = @nick OR seller = @nick LIMIT Transaction.quantity";
            else if (type == "buy" && open)
                _command.CommandText = "SELECT * FROM Transactions WHERE buyer = @nick AND seller IS NULL";
            else if (type == "buy" && !open)
                _command.CommandText = "SELECT * FROM Transactions WHERE buyer = @nick AND seller IS NOT NULL";
            else if (type == "sell" && open)
                _command.CommandText = "SELECT * FROM Transactions WHERE seller = @nick AND buyer IS NULL";
            else if (type == "sell" && !open)
                _command.CommandText = "SELECT transactionID, seller, buyer FROM Transaction WHERE seller = @nick AND buyer IS NOT NULL";
            _command.Parameters.Add(new SQLiteParameter("@nick", nickname));

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
