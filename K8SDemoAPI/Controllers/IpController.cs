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
    [Route("api/v1/ip")]
    public class IpController : Controller
    {
        // GET: api/v1/ip
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var addresslist = Dns.GetHostAddresses(Environment.MachineName);
            return addresslist.Select(o => o.ToString());
        }
        // GET: api/v1/ip/machineName
        [HttpGet("{machineName}")]
        public IEnumerable<string> Get(string machineName)
        {
            var addresslist = Dns.GetHostAddresses(machineName);
            return addresslist.Select(o => o.ToString());
        }
    }
}