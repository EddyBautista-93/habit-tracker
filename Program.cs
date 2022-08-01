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
        // Method used to create a connection to the database.
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;

            // Create a new db connection.
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

        // Table Creation
        static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            string CreateSQLStatement = "CREATE TABLE SampleTable(Col1 VARCHAR(20), Col2 INT)";
            string CreateSQLStatement1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = CreateSQLStatement;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = CreateSQLStatement1;
            sqlite_cmd.ExecuteNonQuery();
        }

        // Insert data.
        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES ('Test Text ', 1);";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES ('Test1 Text1 ', 2);";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES ('Test2 Text2 ', 3);";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES ('Test3 Text3 ', 3);";
            sqlite_cmd.ExecuteNonQuery();
        }

        // Read Data
        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_dr;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            // loop through and read the table
            sqlite_dr = sqlite_cmd.ExecuteReader();
            while (sqlite_dr.Read())
            {
                string myReader = sqlite_dr.GetString(0);
                Console.WriteLine(myReader);
            }
            conn.Clone();
        }

        static void Main(string[] args)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            CreateTable(sqlite_conn);
            InsertData(sqlite_conn);
            ReadData(sqlite_conn);
        }
    }
}
