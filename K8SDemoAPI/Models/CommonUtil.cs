using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace K8SDemoAPI.Models
{
    public class CommonUtil
    {
        public static string GetLocalIp()
        {
            return GetResolvedIp(Environment.MachineName);
        }
        public static string GetResolvedIp(string machineName)
        {
            var addresslist = Dns.GetHostAddresses(machineName);
            var address = addresslist.FirstOrDefault();
            return address != null ? address.ToString() : machineName;
        }
    }
}
