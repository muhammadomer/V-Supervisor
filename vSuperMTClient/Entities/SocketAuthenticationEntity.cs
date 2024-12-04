using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class SocketAuthenticationEntity
    {
        public SocketAuthenticationEntity()
        {
           
        }
        public string ServerIp { get; set; }
        public string WSURI { get; set; }
        public string UserName { get; set; }
        public string UFID { get; set; }
        public string Password { get; set; }
        
        
    }
}