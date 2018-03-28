﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Database
{
    public class Database
    {
        private static SQLiteConnection connection;
        private static SQLiteCommand command;
        private static SQLiteTransaction transaction;
        private static SQLiteDataReader reader;

        public static string DBPath = "database.sqlite";
        public static string SQLPath = "database.sql";

        public struct UserInfo
        {
            string name;
            string nickname;
            string password;
            int numDiginotes;
            int numTransactions;
        }

        public static void StartDB()
        {
            if (!File.Exists(DBPath))
                SQLiteConnection.CreateFile(DBPath);
            
            connection = new SQLiteConnection("Data Source=" + DBPath + ";Version=3;");
            connection.Open();
           
            command = new SQLiteCommand(connection);
        }

        public static bool DeleteDB()
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

        public static bool InsertUser(string name, string nickname, string password)
        {
            try
            {
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

        public static bool InsertDiginote(string nickname)
        {
            try
            {
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
        
        public static bool InsertDiginotes(string nickname, int nDiginotes)
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

        public static bool InsertTransaction(int serialNumber, string seller, string buyer, double price)
        {
            try
            {
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

        public static bool CreateTransaction(string seller, string buyer, double price, int nDiginotes)
        {
            try
            {
                List<int>diginotes = new List<int>();

                command.CommandText = "SELECT serialNumber FROM Diginote WHERE nickname = @source ORDER BY serialNumber DESC LIMIT @num";
                command.Parameters.Add(new SQLiteParameter("@source", source));
                command.Parameters.Add(new SQLiteParameter("@num", nDiginotes));
                reader = com.ExecuteReader();

                while (reader.Read())
                    diginotes.Add(reader.GetInt32(0));

                if (diginotes.Count < nDiginotes)
                    return false;

                transaction = connection.BeginTransaction();

                foreach (int diginote in diginotes)
                {
                    InsertTransaction(diginote, seller, buyer, price);

                    command.CommandText = "UPDATE Diginote SET nickname=@dest WHERE serialNumber=@serial";
                    command.Parameters.Add(new SQLiteParameter("@dest", destination));
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

        public static bool ChangeDiginoteValue(double power)
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

        public static UserInfo GetUserInfo(string nickname)
        {
            UserInfo userInfo;

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
    }
}