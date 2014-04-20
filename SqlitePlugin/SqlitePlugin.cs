using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;
using CycleProcessControl.Pattern.Model;
using System.Data.SQLite;
namespace SqlitePlugin
{
    public class SqlitePlugin : IDBManager
    {

        public SQLiteConnection con;
        public SQLiteCommand cmd = new SQLiteCommand();

        public SqlitePlugin()
        {
         con = new SQLiteConnection(@"data source=E:\data.db; Version=3;");
         con.Open();
         if (con.State == System.Data.ConnectionState.Open) {
             Console.WriteLine("Connected !!!");
         }
        }

        public void Save(StaticPatternModel SaveData)
        {
            
        }
    }
}
