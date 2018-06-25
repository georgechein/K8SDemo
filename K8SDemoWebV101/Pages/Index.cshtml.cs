using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace K8SDemoWebV101.Pages
{
    public class IndexModel : PageModel
    {
        public string clientIp { get; set; }
        public string apiIp { get; set; }
        public void OnGet()
        {
            this.clientIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            this.apiIp = Environment.GetEnvironmentVariable("K8S_DEMO_WEBAPI_IP");
        }
    }
}
