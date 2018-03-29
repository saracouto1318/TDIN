using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Database
{
    public class Database
    {
        public static Database instance;

        private SQLiteConnection connection;
        private SQLiteCommand command;
        private SQLiteTransaction transaction;
        private SQLiteDataReader reader;

        public static string DBPath = "database.sqlite";
        public static string SQLPath = "database.sql";

        private Database()
        {
            StartDB();
        }

        public static Database GetInstance()
        {
            if (instance == null)
                instance = new Database();
            return instance;
        }

        private void StartDB()
        {
            bool justCreated = false;
            if (!File.Exists(DBPath))
            {
                justCreated = true;
                SQLiteConnection.CreateFile(DBPath);
            }

            Console.Write(DBPath);
            connection = new SQLiteConnection("Data Source=" + DBPath + ";Version=3;");

            command = new SQLiteCommand(connection);
            connection.Open();

            if(justCreated)
            {
                CreateDB();
            }
        }

        private void CreateDB()
        {
            command.CommandText = File.ReadAllText(SQLPath);

            try
            {
                transaction = connection.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                transaction.Rollback();
            }
        }

        private void ConnectDB()
        {
            connection.Open();
        }

        private void CloseDB()
        {
            connection.Close();
        }
        
        public bool DeleteDB()
        {
            command.CommandText = "DELETE FROM User; DELETE FROM Value; DELETE FROM Diginote; DELETE FROM TransactionDiginote; DELETE FROM Session";

            try
            {
                transaction = connection.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SQLiteException e) {
                transaction.Rollback();
                return false;
            }
        }

        #region User

        public bool CheckUser(string nickname, string password)
        {
            Console.WriteLine("Check user {0} {1}", nickname, password);

            command.CommandText = "SELECT nickname FROM User WHERE nickname = @nick AND password = @pass ";
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));
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
                return false;
            }
        }

        public bool InsertUser(string name, string nickname, string password)
        {
            Console.WriteLine("Insert user {0} {1} {2}", nickname, password, name);

            command.CommandText = "INSERT INTO User(nickname, name, password) VALUES (@nick, @name, @password)";
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));
            command.Parameters.Add(new SQLiteParameter("@name", name));
            command.Parameters.Add(new SQLiteParameter("@password", password));

            try
            {
                transaction = connection.BeginTransaction();
                int rowCount = command.ExecuteNonQuery();
                transaction.Commit();
                // If number of affected rows is lower than 1 return false
                return rowCount >= 1;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
                return false;
            }
        }

        public User GetUserInfo(string nickname)
        {
            User userInfo = new User();

            command.CommandText = "SELECT nickname, name, password, COUNT(serialNumber), COUNT(transactionID) FROM User, Diginote, Transaction WHERE User.nickname = @nick AND User.nickname = Diginote.nickname AND (Transaction.seller = User.nickname OR Transaction.buyer = User.nickname)";
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    userInfo.username = reader.GetString(0);
                    userInfo.name = reader.GetString(1);
                    userInfo.password = reader.GetString(2);
                    userInfo.numDiginotes = reader.GetInt32(3);
                    userInfo.numTransactions = reader.GetInt32(4);
                }

                reader.Close();

                return userInfo;
            }
            catch (Exception e)
            {
                return userInfo;
            }
        }

        public bool ChangeName(string name, string nickname)
        {
            command.CommandText = "UPDATE User SET name=@name WHERE nickname=@nick";
            command.Parameters.Add(new SQLiteParameter("@name", name));
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                transaction = connection.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SQLiteException e)
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool ChangeUsername(string newNickname, string oldNickname)
        {
            command.CommandText = "UPDATE User SET nickname=@nickNew WHERE nickname=@nickOld";
            command.Parameters.Add(new SQLiteParameter("@nickNew", newNickname));
            command.Parameters.Add(new SQLiteParameter("@nickOld", oldNickname));
            try
            {
                transaction = connection.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SQLiteException e)
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool ChangePassword(string password, string nickname)
        {
            command.CommandText = "UPDATE User SET password=@pass WHERE nickname=@nick";
            command.Parameters.Add(new SQLiteParameter("@pass", password));
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                transaction = connection.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SQLiteException e)
            {
                transaction.Rollback();
                return false;
            }
        }

        #endregion

        #region Session

        public bool InsertSession(string nickname, string session)
        {
            command.CommandText = "INSERT INTO Session(sessionID, nickname) VALUES (@session, @nick)";
            command.Parameters.Add(new SQLiteParameter("@session", session));
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                transaction = connection.BeginTransaction();
                int rowCount = command.ExecuteNonQuery();
                transaction.Commit();
                // If number of affected rows is lower than 1 return false
                return rowCount >= 1;
            }
            catch (SQLiteException e)
            {
                transaction.Rollback();
                return false;
            }
        }

        public string GetUsernameBySession(string session)
        {
            command.CommandText = "SELECT nickname FROM Session WHERE sessionID = @session";
            command.Parameters.Add(new SQLiteParameter("@session", session));

            try
            {
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public void DeleteSession(string nickname)
        {
            command.CommandText = "DELETE FROM Session WHERE nickname=@nick";
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                transaction = connection.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (SQLiteException)
            {
            }
        }

        #endregion

        #region Diginote

        public bool InsertDiginote(string nickname)
        {
            command.CommandText = "INSERT INTO Diginote(owner, facialValue) VALUES (@owner, @facialValue)";
            command.Parameters.Add(new SQLiteParameter("@owner", nickname));
            command.Parameters.Add(new SQLiteParameter("@facialValue", 1));

            try
            {
                transaction = connection.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SQLiteException e)
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool InsertDiginotes(string nickname, int nDiginotes)
        {
            for (int i = 0; i < nDiginotes; i++)
                InsertDiginote(nickname);
            return true;
        }

        public bool ChangeDiginoteValue(double power)
        {
            command.CommandText = "UPDATE Value SET power=@power, quantity=@quantity WHERE power=(SELECT power FROM Value)";
            command.Parameters.Add(new SQLiteParameter("@power", power));
            command.Parameters.Add(new SQLiteParameter("@quantity", 0));

            try
            {
                transaction = connection.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SQLiteException e)
            {
                transaction.Rollback();
                return false;
            }
        }

        public double GetValue()
        {
            double value = 0;
            command.CommandText = "SELECT power FROM Value";
            try
            {
                reader = command.ExecuteReader();

                if (reader.Read())
                    value += reader.GetDouble(0);
                return value;
            }
            catch (SQLiteException e)
            {
                return value;
            }
        }

        #endregion

        #region Transaction

        public bool InsertTransaction(int serialNumber, int nDiginotes)
        {
            for (int i = 0; i < nDiginotes; i++)
                command.CommandText += "INSERT INTO Transaction(diginoteID, seller, buyer, price, quantity) VALUES (@serial, @seller, @buyer, @price, @quantity)";

            command.Parameters.Add(new SQLiteParameter("@serial", serialNumber));
            command.Parameters.Add(new SQLiteParameter("@seller", null));
            command.Parameters.Add(new SQLiteParameter("@buyer", null));
            command.Parameters.Add(new SQLiteParameter("@price", nDiginotes * this.GetValue()));
            command.Parameters.Add(new SQLiteParameter("@quantity", nDiginotes));

            try
            {
                transaction = connection.BeginTransaction();
                command.ExecuteNonQuery();
                transaction.Commit();

                return true;
            }
            catch (SQLiteException e)
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool CompleteTransaction(string seller, string buyer, int nDiginotes)
        {
            try
            {
                List<int> diginotes = new List<int>();

                command.CommandText = "SELECT serialNumber FROM Diginote WHERE nickname = @source ORDER BY serialNumber DESC LIMIT @num";
                command.Parameters.Add(new SQLiteParameter("@source", seller));
                command.Parameters.Add(new SQLiteParameter("@num", nDiginotes));

                reader = command.ExecuteReader();

                while (reader.Read())
                    diginotes.Add(reader.GetInt32(0));

                if (diginotes.Count < nDiginotes)
                    return false;

                transaction = connection.BeginTransaction();

                foreach (int diginote in diginotes)
                {
                    command.CommandText = "UPDATE Diginote SET nickname=@dest WHERE serialNumber=@serial";
                    command.Parameters.Add(new SQLiteParameter("@dest", buyer));
                    command.Parameters.Add(new SQLiteParameter("@serial", diginote));
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE TransactionDiginote SET buyer=@buyer, seller=@seller, price=@price WHERE diginoteID=@serial";
                    command.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                    command.Parameters.Add(new SQLiteParameter("@seller", seller));
                    command.Parameters.Add(new SQLiteParameter("@price", nDiginotes * this.GetValue()));
                    command.Parameters.Add(new SQLiteParameter("@serial", diginote));
                    command.ExecuteNonQuery();
                }

                transaction.Commit();

                command.CommandText = "SELECT quantity FROM Value";
                reader = command.ExecuteReader();

                int currentQuantity = 0;

                if (reader.Read())
                    currentQuantity += reader.GetInt32(0);

                command.CommandText = "UPDATE Value SET quantity=@quantity WHERE power=(SELECT power FROM Value)";
                command.Parameters.Add(new SQLiteParameter("@quantity", currentQuantity + 1));
                command.ExecuteNonQuery();

                return true;
            }
            catch (SQLiteException e)
            {
                transaction.Rollback();
                return false;
            }
        }

        public List<Transaction> GetTransactions(string type, bool open, string nickname)
        {
            List<Transaction> aux = new List<Transaction>();
            if (type == "all")
                command.CommandText = "SELECT * FROM Transaction WHERE buyer = @nick OR seller = @nick LIMIT Transaction.quantity";
            else if (type == "buy" && open)
                command.CommandText = "SELECT * FROM Transaction WHERE buyer = @nick AND seller IS NULL";
            else if (type == "buy" && !open)
                command.CommandText = "SELECT * FROM Transaction WHERE buyer = @nick AND seller IS NOT NULL";
            else if (type == "sell" && open)
                command.CommandText = "SELECT * FROM Transaction WHERE seller = @nick AND buyer IS NULL";
            else if (type == "sell" && !open)
                command.CommandText = "SELECT transactionID, seller, buyer FROM Transaction WHERE seller = @nick AND buyer IS NOT NULL";
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                reader = command.ExecuteReader();

                Transaction info = new Transaction();

                while (reader.Read())
                {
                    info.ID = 0;
                    info.buyer = reader.GetString(3);
                    info.seller = reader.GetString(2);
                    info.value = reader.GetDouble(4);
                    info.quantity = reader.GetInt32(5);
                    aux.Add(info);
                }

                List<Transaction> transactions = new List<Transaction>();
                for (int i = 0; i < aux.Count;)
                {
                    info.ID = i + 1;
                    info.buyer = aux[i].buyer;
                    info.seller = aux[i].seller;
                    info.value = aux[i].value;
                    info.quantity = aux[i].quantity;

                    transactions.Add(info);
                    i += aux[i].quantity;
                }

                return transactions;
            }
            catch (SQLiteException e)
            {
                return null;
            }
        }

        public bool DeleteTransactions(int transactionID, int quantity)
        {
            try
            {
                for (int i = 0; i < quantity; i++)
                {
                    transactionID += i;
                    command.CommandText += "DELETE FROM TransactionDiginote WHERE TransactionID=@ID";
                    command.Parameters.Add(new SQLiteParameter("@ID", transactionID));
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (SQLiteException e)
            {
                return false;
            }
        }

        #endregion
    }
}
