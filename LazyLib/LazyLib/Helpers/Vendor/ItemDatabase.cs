namespace LazyLib.Helpers.Vendor
{
    using LazyLib;
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class ItemDatabase
    {
        private static readonly SQLiteConnection Db = new SQLiteConnection("Data Source=database.db3");

        private static void CheckSchema()
        {
            Query("CREATE TABLE IF NOT EXISTS items (id INTEGER PRIMARY KEY NOT NULL, item_id INTEGER UNIQUE, item_name VARCHAR(255) UNIQUE, item_quality VARCHAR(255));");
        }

        public static void ClearDatabase()
        {
            Logging.Write("Clearing database", new object[0]);
            Query("DELETE FROM items");
        }

        public static void Close()
        {
            IsOpen = false;
            Db.Close();
        }

        public static DataRow GetItem(string id) => 
            QueryFetchRow($"SELECT * FROM items WHERE item_id = '{id}'");

        public static void Open()
        {
            IsOpen = true;
            Db.Open();
            CheckSchema();
        }

        public static void PutItem(string id, string name, string quality)
        {
            name = name.Replace("'", "''");
            name = name.Replace("\"", "\"\"");
            if (QueryInsert($"INSERT INTO items (item_id, item_name, item_quality) VALUES ('{id}', '{name}', '{quality}')") >= 0)
            {
                object[] args = new object[] { name, id };
                Logging.Debug("Database: Adding {0} to Database with id = {1}", args);
            }
        }

        private static void Query(string sql)
        {
            SQLiteCommand command = Db.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        private static DataRow QueryFetchRow(string sql)
        {
            SQLiteCommand cmd = Db.CreateCommand();
            cmd.CommandText = sql;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            try
            {
                dataTable.BeginLoadData();
                adapter.Fill(dataTable);
                dataTable.EndLoadData();
            }
            catch (Exception exception)
            {
                Logging.Write("Exception in QueryFetchRow: " + exception, new object[0]);
            }
            finally
            {
                adapter.Dispose();
            }
            return dataTable.Rows.Cast<DataRow>().FirstOrDefault<DataRow>();
        }

        private static int QueryInsert(string sql)
        {
            int num;
            SQLiteCommand command = Db.CreateCommand();
            command.CommandText = sql + "; SELECT last_insert_rowid() AS RecordID;";
            try
            {
                num = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception exception)
            {
                Logging.Debug("Exception in QueryInsert: [error] " + exception, new object[0]);
                Logging.Debug("Exception in QueryInsert: [sql] " + sql, new object[0]);
                num = -1;
            }
            Logging.Debug("QueryInsert: " + num, new object[0]);
            return num;
        }

        public static bool IsOpen { get; private set; }
    }
}

