using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace K8SDemoWebV1.Pages
{
    public class IndexModel : PageModel
    {
        public string webIp { get; set; }
        public string apiIp { get; set; }
        public void OnGet()
        {
            var addresslist = Dns.GetHostAddresses(Environment.MachineName);
            var tmpIp = addresslist.Select(o => o.ToString()).FirstOrDefault();
            this.webIp = string.IsNullOrEmpty(tmpIp) ? string.Empty : tmpIp;
            this.apiIp = Environment.GetEnvironmentVariable("K8S_DEMO_WEBAPI_IP");
        }
    }
}
