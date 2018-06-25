using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace K8SDemoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/env")]
    public class EnvController : Controller
    {
        // GET: api/v1/env
        [HttpGet]
        public string Get()
        {
            return "Please give a environment variable name.";
        }
        // GET: api/v1/env/myname
        [HttpGet("{varName}")]
        public string Get(string varName)
        {
            var value = string.Empty;
            try
            {
                value = Environment.GetEnvironmentVariable(varName);
            }
            catch
            {
                value = string.Format("Environment variable:{0} isn't avaiable.", varName);
            }
            return value;
        }
    }
}