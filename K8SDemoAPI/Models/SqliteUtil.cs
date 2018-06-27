using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using System.IO;

namespace K8SDemoAPI.Models
{
    public class SqliteUtil
    {
        private string _ConnStr;
        private string _DbPath;
        public SqliteUtil(string dbPath)
        {
            this._DbPath = dbPath;
            this._ConnStr = string.Format("data source={0}", dbPath);
        }
        /// <summary>
        /// 資料庫檔案不存在時，自動建立相關資料表
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="sql4CreateTables"></param>
        public SqliteUtil(string dbPath, string[] sql4CreateTables)
        {
            this._DbPath = dbPath;
            this._ConnStr = string.Format("data source={0}", dbPath);
            if (File.Exists(dbPath))
                return;
            using (var cn = this.GetConn())
            {
                foreach (var sql in sql4CreateTables)
                {
                    cn.Execute(sql);
                }
            }
        }
        public string ConnStr
        {
            get
            {
                return this._ConnStr;
            }
        }
        public void ExecuteSqls(string[] sqls)
        {
            using (var cn = this.GetConn())
            {
                foreach (var sql in sqls)
                {
                    cn.Execute(sql);
                }
            }
        }
        public void ExecuteSql(string sql)
        {
            using (var cn = this.GetConn())
            {
                cn.Execute(sql);
            }
        }
        /// <summary>
        /// 因參數是用@paramName，要注意參數名稱和傳入物件的欄位名稱需相同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertSql">新增用的SQL</param>
        /// <param name="dataObject">資料物件</param>
        public void Insert<T>(string insertSql, T[] dataObject)
        {
            using (var cn = this.GetConn())
            {
                cn.Execute(insertSql, dataObject);
            }
        }
        public int GetCount(string tableName)
        {
            using (var cn = this.GetConn())
            {
                return cn.Query<int>(string.Format("SELECT count(1) FROM {0}", tableName)).FirstOrDefault();
            }
        }
        public void ClearTable(string tableName)
        {
            using (var cn = this.GetConn())
            {
                cn.Execute(string.Format("DELETE FROM {0}", tableName));
            }
        }
        public List<T> SelectAll<T>(string table)
        {
            using (var cn = this.GetConn())
            {
                return cn.Query<T>(string.Format("SELECT * FROM {0}", table)).ToList();
            }
        }
        public List<T> Select<T>(string table, string criteria)
        {
            using (var cn = this.GetConn())
            {
                return cn.Query<T>(string.Format("SELECT * FROM {0} WHERE {1}", table, criteria)).ToList();
            }
        }
        public SqliteConnection GetConn()
        {
            return new SqliteConnection(this.ConnStr);
        }
    }
}
