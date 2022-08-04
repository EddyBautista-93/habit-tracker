using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habit_tracker
{
    class Program
    {
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;

            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
            try
            {
                sqlite_conn.Open();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }
            return sqlite_conn;
        }

        static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            string CreateSQLStatement = "CREATE TABLE HabitTable(Habit VARCHAR(20), StartDate VARCHAR(20))";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = CreateSQLStatement;
            sqlite_cmd.ExecuteNonQuery();

        }

        // Insert data.
        static void InsertData(SQLiteConnection conn)
        {
            Console.WriteLine("What habit do you want to add?");
            string habit = Console.ReadLine();
            Console.WriteLine("When will you start this habit");
            string startDate = Console.ReadLine();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO  HabitTable(Habit, StartDate) VALUES (' " + habit + " ', '" + startDate + "');";
            sqlite_cmd.ExecuteNonQuery();
        }

        // Read Data
        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_dr;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM HabitTable";

            // loop through and read the table
            sqlite_dr = sqlite_cmd.ExecuteReader();
            while (sqlite_dr.Read())
            {
                string myReader = sqlite_dr.GetString(0);
                string myReader1 = sqlite_dr.GetString(1);
                Console.WriteLine(myReader, myReader1);
            }
            conn.Clone();
        }

        static void Main(string[] args)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            // CreateTable(sqlite_conn);
            Console.WriteLine("Main Menu");         
            Console.WriteLine("---------");
            Console.WriteLine("Type 0 to close application");
            Console.WriteLine("Type 1 to View All Habits");
            Console.WriteLine("Type 2 to Add A Habit");
            Console.WriteLine("Type 3 to Delete a Habit");
            Console.WriteLine("Type 4 to Update a Habit");
            
            string choice = Console.ReadLine();
            Console.WriteLine("You Picked " + choice);

            switch (choice)
            {
                case "1":
                ReadData(sqlite_conn);
                    break;
                case "2":
                InsertData(sqlite_conn);
                ReadData(sqlite_conn);
                    break;
                case "3":

                default:
                    break;
            }            
        }
    }
}
