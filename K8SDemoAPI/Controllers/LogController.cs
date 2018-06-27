using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using K8SDemoAPI.Models;

namespace K8SDemoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/log")]
    public class LogController : Controller
    {
        // GET: api/Log
        [HttpGet]
        public RequestLog[] Get()
        {
            var sqliteUtil = new SqliteUtil(this.GetDbPath(), RequestLogUtil.GetSql4CreateTables());
            var result = sqliteUtil.Select<RequestLogInDb>("RequestLog",
                                " CreatedOn >= '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'"
                                ).OrderByDescending(o => o.CreatedOn).ToList();
            var entities = new RequestLogUtil().ConvertTo(result);
            return entities.ToArray();
        }

        // GET: api/Log/5
        [HttpGet("{id}")]
        public RequestLog Get(string id)
        {
            var sqliteUtil = new SqliteUtil(this.GetDbPath(), RequestLogUtil.GetSql4CreateTables());
            //demo用，注意SQLInjection。
            var result = sqliteUtil.Select<RequestLog>("RequestLog",
                                " Id = '" + id + "'"
                                ).FirstOrDefault();
            return result != null ? result :　new RequestLog() { Id = "", WebIp = "", ApiIp = "", CreatedOn = DateTime.Now } ;
        }
        
        // POST: api/Log
        [HttpPost]
        public void Post([FromBody]RequestLog value)
        {
            var sqliteUtil = new SqliteUtil(this.GetDbPath(), RequestLogUtil.GetSql4CreateTables());
            var entity = new RequestLogUtil().ConvertTo(value);
            sqliteUtil.Insert<RequestLogInDb>(RequestLogUtil.GetSql4Insert(), new RequestLogInDb[] { entity });
        }
        
        // PUT: api/v1/log/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private string GetDbPath()
        {
            return Environment.GetEnvironmentVariable("K8S_DEMO_DB_PATH");
        }
    }
}
