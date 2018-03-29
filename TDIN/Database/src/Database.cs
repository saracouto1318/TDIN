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

        public static string DBPath = "database.sql";
        public static string SQLPath = "database.sqlite";

        public struct UserInfo
        {
            public string name;
            public string nickname;
            public string password;
            public int numDiginotes;
            public int numTransactions;
        }

        public struct TransactionInfo
        {
            public int ID;
            public int quantity;
            public double value;
            public string buyer;
            public string seller;
        }

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
            if (!File.Exists(DBPath))
                SQLiteConnection.CreateFile(DBPath);

            Console.Write(DBPath);
            connection = new SQLiteConnection("Data Source=" + DBPath + ";Version=3;");

            command = new SQLiteCommand(connection);
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

        public bool InsertUser(string name, string nickname, string password)
        {
            command.CommandText = "INSERT INTO User(nickname, name, password) VALUES (@nick, @name, @password)";
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));
            command.Parameters.Add(new SQLiteParameter("@name", name));
            command.Parameters.Add(new SQLiteParameter("@password", password));

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

        public bool CheckUser(string username, string password)
        {
            command.CommandText = "SELECT nickname FROM User WHERE nickname = @nick AND password = @pass ";
            command.Parameters.Add(new SQLiteParameter("@nick", username));
            command.Parameters.Add(new SQLiteParameter("@pass", password));

            try
            {
                reader = command.ExecuteReader();

                if (reader.Read())
                    return true;
                return false;
            }
            catch (SQLiteException e)
            {
                transaction.Rollback();
                return false;
            }
        }

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
            catch (SQLiteException e) {
                transaction.Rollback();
                return false;
            }
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
            catch(SQLiteException e)
            {
                return value;
            }
        }

        public UserInfo GetUserInfo(string nickname)
        {
            UserInfo userInfo;
            userInfo.name = "";
            userInfo.nickname = "";
            userInfo.password = "";
            userInfo.numDiginotes = 0;
            userInfo.numTransactions = 0;

            command.CommandText = "SELECT nickname, name, password, COUNT(serialNumber), COUNT(transactionID) FROM User, Diginote, Transaction WHERE User.nickname = @nick AND User.nickname = Diginote.nickname AND (Transaction.seller = User.nickname OR Transaction.buyer = User.nickname)";
            command.Parameters.Add(new SQLiteParameter("@nick", nickname));

            try
            {
                reader = command.ExecuteReader();
               
                if (reader.Read()) {
                    userInfo.nickname = reader.GetString(0);
                    userInfo.name = reader.GetString(1);
                    userInfo.password = reader.GetString(2);
                    userInfo.numDiginotes = reader.GetInt32(3);
                    userInfo.numTransactions = reader.GetInt32(4);
                }
                
                return userInfo;
            }
            catch (Exception e) {
                return userInfo;
            }
        }

        public List<TransactionInfo> GetTransactions(string type, bool open, string nickname)
        {
            List<TransactionInfo> aux = new List<TransactionInfo>();
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

                TransactionInfo info;

                while (reader.Read())
                {
                    info.ID = 0;
                    info.buyer = reader.GetString(3);
                    info.seller = reader.GetString(2);
                    info.value = reader.GetDouble(4);
                    info.quantity = reader.GetInt32(5);
                    aux.Add(info);
                }

                List<TransactionInfo> transactions = new List<TransactionInfo>();
                for(int i=0; i < aux.Count; )
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
            catch(SQLiteException e)
            {
                return false;
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

    }
}
