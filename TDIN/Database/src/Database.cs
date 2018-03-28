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
        }
        
        public bool DeleteDB()
        {
            try
            {
                transaction = connection.BeginTransaction();
                command.CommandText = "DELETE FROM User; DELETE FROM Value; DELETE FROM Diginote; DELETE FROM TransactionDiginote; DELETE FROM Session";
                command.ExecuteNonQuery();
                transaction.Commit();

                return true;
            }
            catch (Exception e) {
                return false;
            }

        }

        public bool InsertUser(string name, string nickname, string password)
        {
            try
            {
                Console.Write(name + " " + nickname + " " + password);
                connection.Open();

                command.CommandText = "INSERT INTO User(nickname, name, password) VALUES (@nick, @name, @password)";
                command.Parameters.Add(new SQLiteParameter("@nick", nickname));
                command.Parameters.Add(new SQLiteParameter("@name", name));
                command.Parameters.Add(new SQLiteParameter("@password", password));

                command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CheckUser(string username, string password)
        {
            try
            {
                command.CommandText = "SELECT nickname FROM User WHERE nickname = @nick AND password = @pass ";
                command.Parameters.Add(new SQLiteParameter("@nick", username));
                command.Parameters.Add(new SQLiteParameter("@pass", password));
                reader = command.ExecuteReader();

                if (reader.Read())
                    return true;
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool InsertDiginote(string nickname)
        {
            try
            {
                connection.Open();

                command.CommandText = "INSERT INTO Diginote(owner, facialValue) VALUES (@owner, @facialValue)";
                command.Parameters.Add(new SQLiteParameter("@owner", nickname));
                command.Parameters.Add(new SQLiteParameter("@facialValue", 1));

                command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
        
        public bool InsertDiginotes(string nickname, int nDiginotes)
        {
            try
            {
                transaction = connection.BeginTransaction();

                for (int i = 0; i < nDiginotes; i++)
                    InsertDiginote(nickname);

                transaction.Commit();

                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool InsertTransaction(int serialNumber, string seller, string buyer, double price)
        {
            try
            {
                connection.Open();

                command.CommandText = "INSERT INTO Transaction(diginoteID, seller, buyer, price) VALUES (@serial, @seller, @buyer, @price)";
                command.Parameters.Add(new SQLiteParameter("@serial", serialNumber));
                command.Parameters.Add(new SQLiteParameter("@seller", seller));
                command.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                command.Parameters.Add(new SQLiteParameter("@price", price));

                command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CreateTransaction(string seller, string buyer, double price, int nDiginotes)
        {
            try
            {
                List<int>diginotes = new List<int>();

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
                    InsertTransaction(diginote, seller, buyer, price);

                    command.CommandText = "UPDATE Diginote SET nickname=@dest WHERE serialNumber=@serial";
                    command.Parameters.Add(new SQLiteParameter("@dest", buyer));
                    command.Parameters.Add(new SQLiteParameter("@serial", diginote));
                    command.ExecuteNonQuery();
                }
                transaction.Commit();

                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool ChangeDiginoteValue(double power)
        {
            try
            {
                command.CommandText = "UPDATE Value SET power=@power WHERE power=(SELECT power FROM Value)";
                command.Parameters.Add(new SQLiteParameter("@power", power));
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
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

            try
            {
                command.CommandText = "SELECT nickname, name, password, COUNT(serialNumber), COUNT(transactionID) FROM User, Diginote, Transaction WHERE User.nickname = @nick AND User.nickname = Diginote.nickname AND (Transaction.seller = User.nickname OR Transaction.buyer = User.nickname)";
                command.Parameters.Add(new SQLiteParameter("@nick", nickname));
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

        public bool ChangeName(string name, string nickname)
        {
            try
            {
                command.CommandText = "UPDATE User SET name=@name WHERE nickname=@nick";
                command.Parameters.Add(new SQLiteParameter("@name", name));
                command.Parameters.Add(new SQLiteParameter("@nick", nickname));
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeUsername(string newNickname, string oldNickname)
        {
            try
            {
                command.CommandText = "UPDATE User SET nickname=@nickNew WHERE nickname=@nickOld";
                command.Parameters.Add(new SQLiteParameter("@nickNew", newNickname));
                command.Parameters.Add(new SQLiteParameter("@nickOld", oldNickname));
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangePassword(string password, string nickname)
        {
            try
            {
                command.CommandText = "UPDATE User SET password=@pass WHERE nickname=@nick";
                command.Parameters.Add(new SQLiteParameter("@pass", password));
                command.Parameters.Add(new SQLiteParameter("@nick", nickname));
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
