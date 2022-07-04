using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Windows.Storage;

namespace DataAccessLibrary
{
    public static class DataAccess
    {

        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("simple_todo_data.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            Debug.WriteLine(dbpath);
            using (SqliteConnection db =
                new SqliteConnection($"Filename={dbpath}"))
            {   
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS List" +
                    "(Code_List TEXT PRIMARY KEY, Subject TEXT, Type BOOLEAN, Amount_Item INTERGER)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT EXISTS ItNormal" +
                    "(ID INTERGER PRIMARY KEY,Code_List TEXT, Subject TEXT, Description TEXT, Is_Finished BOOLEAN)";

                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT EXISTS ItExtended" +
                    "(ID INTERGER PRIMARY KEY, Code_List TEXT, Subject TEXT, Description TEXT, Priority INTERGER, Date TEXT, Time TEXT, Is_Finished BOOLEAN)";

                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

            }
        }
        // Add List
        public static void AddData_To_List(string codeList, string subject, bool type, int amountItem)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO List VALUES (@codeList,@subject ,@type,@amountItem)";
                insertCommand.Parameters.AddWithValue("@codeList", codeList);
                insertCommand.Parameters.AddWithValue("@subject", subject);
                insertCommand.Parameters.AddWithValue("@type", type);
                insertCommand.Parameters.AddWithValue("@amountItem", amountItem);

                insertCommand.ExecuteReader();
                db.Close();
            }
        }

        //Get List from Database by Code List
        public static List<String> GetData_CodeList()
        {
            List<String> entries = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Code_List from List", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }

                db.Close();
            }

            return entries;
        }

        //Get All List
        public static List<String> GetData_List()
        {
            List<String> entries = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from List", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {   
                    entries.Add(query.GetString(0)+ "," + query.GetString(1) + "," + query.GetString(2) + "," + query.GetString(3));
                }

                db.Close();
            }

            return entries;
        }

        //Xoa mot list trong Database
        public static void Delete_List(string codeList)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using(SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand deleteCommand = new SqliteCommand("DELETE FROM List WHERE Code_List = '" + codeList + "'", db);
                deleteCommand.ExecuteReader();
                db.Close();
            }
        }

        //Update Name
        public static void Update_NameOfList(string codeList, string newName)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand updataCommand = new SqliteCommand("UPDATE List SET Subject = '"+newName+"' WHERE Code_List = '"+codeList+"'",db);
                updataCommand.ExecuteReader();
                db.Close();
            }
        }

        //Update Entities

        public static void Update_EntieyOfList(string codeList, int number)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;
                updateCommand.CommandText = "UPDATE List SET Amount_Item = @number WHERE Code_List = @codeList";
                updateCommand.Parameters.AddWithValue("@number", number);
                updateCommand.Parameters.AddWithValue("@codeList", codeList);
                updateCommand.ExecuteReader();
                db.Close();
            }
        }

        // Count Item Normal
        public static int Count_ItemNormal(string codeList)
        {
            int numberOfListTable = 0;
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");

            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand countCommand = new SqliteCommand("SELECT * FROM ItNormal WHERE Code_List = '" +codeList+"'", db);
                SqliteDataReader count =  countCommand.ExecuteReader();
                
                while (count.Read())
                {
                    numberOfListTable++;
                }
                db.Close();
            }

            return numberOfListTable;
        }

        // Count Item Extended
        public static int Count_ItemExtended(string codeList)
        {
            int numberOfListTable = 0;
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");

            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand countCommand = new SqliteCommand("SELECT * FROM ItExtended WHERE Code_List = '" + codeList + "'", db);
                SqliteDataReader count = countCommand.ExecuteReader();

                while (count.Read())
                {
                    numberOfListTable++;
                }
                db.Close();
            }

            return numberOfListTable;
        }


        ///////////////////////////////////////
        // Handle Item Section
        // Add Item Normal
        public static void AddData_to_NormalItem(int id, string codeList, string subject, string description, bool isFinished)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using(SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO ItNormal VALUES (@id,@codeList, @subject, @description, @isFinished)";
                insertCommand.Parameters.AddWithValue("@id", id);
                insertCommand.Parameters.AddWithValue("@codeList", codeList);
                insertCommand.Parameters.AddWithValue("@subject", subject);
                insertCommand.Parameters.AddWithValue("@description", description);
                insertCommand.Parameters.AddWithValue("@isFinished", isFinished);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        //Check id Normal
        public static List<int> GetData_Id_NormalItem()
        {   
            List<int> list = new List<int>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using(SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectcommand = new SqliteCommand("SELECT ID from ItNormal", db);
                SqliteDataReader query = selectcommand.ExecuteReader();

                while (query.Read())
                {
                    list.Add(query.GetInt32(0));
                }

                db.Close();
            }

            return list;
        }

        //Get Item by Code_List Normal

        public static List<string> GetData_By_CodeList(string codeList)
        {
            List<string> list = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using(SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                selectCommand.CommandText = "SELECT * FROM ItNormal WHERE Code_List = @codeList";
                selectCommand.Parameters.AddWithValue("@codeList", codeList);
                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    list.Add(query.GetString(0)+","+ query.GetString(1)+","+query.GetString(2)+","+query.GetString(3)+","+query.GetString(4));
                }

                db.Close();
            }

            return list;
        }

        // Add Item Extended
        public static void AddData_to_ExtendItem(int id, string codeList, string subject, string description, int priority, string date, string time, bool isFinished)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO ItExtended VALUES (@id, @codeList, @subject, @description,@priority, @date, @time, @isFinished)";
                insertCommand.Parameters.AddWithValue("@id", id);
                insertCommand.Parameters.AddWithValue("@codeList", codeList);
                insertCommand.Parameters.AddWithValue("@subject", subject);
                insertCommand.Parameters.AddWithValue("@description", description);
                insertCommand.Parameters.AddWithValue("@priority", priority);
                insertCommand.Parameters.AddWithValue("@date", date);
                insertCommand.Parameters.AddWithValue("@time", time);
                insertCommand.Parameters.AddWithValue("@isFinished", isFinished);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }
        
        // Get Id Extended
        public static List<int> GetData_Id_ExTendedItem()
        {
            List<int> list = new List<int>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectcommand = new SqliteCommand("SELECT ID from ItExtended", db);
                SqliteDataReader query = selectcommand.ExecuteReader();

                while (query.Read())
                {
                    list.Add(query.GetInt32(0));
                }

                db.Close();
            }

            return list;
        }

        // Get ItemExtended by Code_List

        public static List<string> GetData_By_CodeList1(string codeList)
        {
            List<string> list = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                selectCommand.CommandText = "SELECT * FROM ItExtended WHERE Code_List = @codeList";
                selectCommand.Parameters.AddWithValue("@codeList", codeList);
                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    list.Add(query.GetString(0) + "," + query.GetString(1) + "," + query.GetString(2) + "," + query.GetString(3) + "," + query.GetString(4) + "," + query.GetString(5) + "," + query.GetString(6) + "," + query.GetString(7));
                }

                db.Close();
            }

            return list;
        }

        // Delete All Item Normal

        public static void Delete_All_Item_Normal(string codeList)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand("DELETE FROM ItNormal WHERE Code_List = '" + codeList + "'", db);
                deleteCommand.ExecuteReader();

                db.Close();
            }
        }

        // Delete All Item Normal

        public static void Delete_All_Item_Extended(string codeList)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand("DELETE FROM ItExtended WHERE Code_List = '" + codeList + "'", db);
                deleteCommand.ExecuteReader();

                db.Close();
            }
        }

        // Delete Item by Id
        public static void Delete_Item_Normal_Id(int id)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand deleteCommand = new SqliteCommand("DELETE FROM ItNormal WHERE ID = '" + id + "'", db);
                deleteCommand.ExecuteReader();
                db.Close();
            }
        }
        public static void Delete_Item_Extended_Id(int id)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand deleteCommand = new SqliteCommand("DELETE FROM ItExtended WHERE ID = '" + id + "'", db);
                deleteCommand.ExecuteReader();
                db.Close();
            }
        }


        // Update Item 

        public static void Update_Item_Extened(int id, string subject, string description, int priority, string date, string time)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;

                updateCommand.CommandText = "UPDATE ItExtended SET Subject = @subject, Description = @description, Priority = @priority, Date = @date, Time = @time WHERE ID = @id";
                updateCommand.Parameters.AddWithValue("@subject", subject);
                updateCommand.Parameters.AddWithValue("@description", description);
                updateCommand.Parameters.AddWithValue("@priority", priority);
                updateCommand.Parameters.AddWithValue("@date", date);
                updateCommand.Parameters.AddWithValue("@time", time);
                updateCommand.Parameters.AddWithValue("@id", id);
                updateCommand.ExecuteReader();
                db.Close();
            }
        }
        public static void Update_Item_Normal(int id, string subject, string description)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;

                updateCommand.CommandText = "UPDATE ItNormal SET Subject = @subject, Description = @description WHERE ID = @id";
                updateCommand.Parameters.AddWithValue("@subject", subject);
                updateCommand.Parameters.AddWithValue("@description", description);
                updateCommand.Parameters.AddWithValue("@id", id);
                updateCommand.ExecuteReader();
                db.Close();
            }
        }

        public static void Update_Item_Normal_Finish(int id, bool is_Finished)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;

                updateCommand.CommandText = "UPDATE ItNormal SET Is_Finished = @is_Finished WHERE ID = @id";
                updateCommand.Parameters.AddWithValue("@is_Finished", is_Finished);
                updateCommand.Parameters.AddWithValue("@id", id);
                updateCommand.ExecuteReader();
                db.Close();
            }
        }
        public static void Update_Item_Extended_Finish(int id, bool is_Finished)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "simple_todo_data.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;

                updateCommand.CommandText = "UPDATE ItExtended SET Is_Finished = @is_Finished WHERE ID = @id";
                updateCommand.Parameters.AddWithValue("@is_Finished", is_Finished);
                updateCommand.Parameters.AddWithValue("@id", id);
                updateCommand.ExecuteReader();
                db.Close();
            }
        }
    }
}
