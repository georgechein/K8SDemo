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
                WebIp = obj.WebIp,
                ApiIp = string.IsNullOrEmpty(obj.ApiIp) ? CommonUtil.GetLocalIp() : obj.ApiIp
            };
        }
        public RequestLog ConvertTo(RequestLogInDb obj)
        {
            return new RequestLog()
            {
                Id = obj.Id,
                CreatedOn = obj.CreatedOn,
                WebIp = obj.WebIp,
                ApiIp = obj.ApiIp
            };
        }
        public static string[] GetSql4CreateTables()
        {
            return new string[] { @"
                CREATE TABLE RequestLog (
                Id VARCHAR(32),
                WebIp VARCHAR(32),
                ApiIp VARCHAR(32),
                CreatedOn DATETIME, 
                CONSTRAINT RequestLog_PK PRIMARY KEY (Id)
                )" };
        }
        public static string GetSql4Insert()
        {
            return "INSERT INTO RequestLog VALUES (@Id, @WebIp, @ApiIp, @CreatedOn)";
        }
    }
    public class RequestLog
    {
        public string Id { get; set; }
        /// <summary>
        /// Enduser IP, not web server IP
        /// </summary>
        public string WebIp { get; set; }
        /// <summary>
        /// WebAPI Server IP, not web server IP
        /// </summary>
        public string ApiIp { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class RequestLogInDb
    {
        public string Id { get; set; }
        /// <summary>
        /// Web server IP
        /// </summary>
        public string WebIp { get; set; }
        /// <summary>
        /// WebAPI Server IP
        /// </summary>
        public string ApiIp { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
