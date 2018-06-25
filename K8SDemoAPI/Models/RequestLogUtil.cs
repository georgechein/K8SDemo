using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K8SDemoAPI.Models
{
    public class RequestLogUtil
    {
        public List<RequestLog> ConvertTo(List<RequestLogInDb> sourceList)
        {
            var newList = new List<RequestLog>();
            foreach (var item in sourceList)
            {
                newList.Add(ConvertTo(item));
            }
            return newList;
        }
        public List<RequestLogInDb> ConvertTo(List<RequestLog> sourceList)
        {
            var newList = new List<RequestLogInDb>();
            foreach (var item in sourceList)
            {
                newList.Add(ConvertTo(item));
            }
            return newList;
        }

        public RequestLogInDb ConvertTo(RequestLog obj)
        {
            return new RequestLogInDb()
            {
                Id = string.IsNullOrEmpty(obj.Id) ? Guid.NewGuid().ToString("N") : obj.Id,
                CreatedOn = obj.CreatedOn.HasValue ? obj.CreatedOn.Value : DateTime.Now,
                ClientIp = obj.ClientIp,
                ServerIp = string.IsNullOrEmpty(obj.ServerIp) ? CommonUtil.GetLocalIp() : obj.ServerIp
            };
        }
        public RequestLog ConvertTo(RequestLogInDb obj)
        {
            return new RequestLog()
            {
                Id = obj.Id,
                CreatedOn = obj.CreatedOn,
                ClientIp = obj.ClientIp,
                ServerIp = obj.ServerIp
            };
        }
        public static string[] GetSql4CreateTables()
        {
            return new string[] { @"
                CREATE TABLE RequestLog (
                Id VARCHAR(32),
                ClientIp VARCHAR(32),
                ServerIp VARCHAR(32),
                CreatedOn DATETIME, 
                CONSTRAINT RequestLog_PK PRIMARY KEY (Id)
                )" };
        }
        public static string GetSql4Insert()
        {
            return "INSERT INTO RequestLog VALUES (@Id, @ClientIp, @ServerIp, @CreatedOn)";
        }
    }
    public class RequestLog
    {
        public string Id { get; set; }
        /// <summary>
        /// Enduser IP, not web server IP
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// WebAPI Server IP, not web server IP
        /// </summary>
        public string ServerIp { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class RequestLogInDb
    {
        public string Id { get; set; }
        /// <summary>
        /// Enduser IP, not web server IP
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// WebAPI Server IP, not web server IP
        /// </summary>
        public string ServerIp { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
