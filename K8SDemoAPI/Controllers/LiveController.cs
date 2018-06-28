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
    [Route("api/v1/live")]
    public class LiveController : Controller
    {
        // GET: api/v1/live
        [HttpGet]
        public bool Get()
        {
            try
            {
                var sqliteUtil = new SqliteUtil(this.GetDbPath(), RequestLogUtil.GetSql4CreateTables());
                var result = sqliteUtil.GetCount("RequestLog");
                if (result >= 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string GetDbPath()
        {
            return Environment.GetEnvironmentVariable("K8S_DEMO_DB_PATH");
        }
    }
}
