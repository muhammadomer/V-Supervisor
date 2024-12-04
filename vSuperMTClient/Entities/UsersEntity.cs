using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class UsersEntity
    {
        public UsersEntity()
        {
            Permissions = new List<int>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public bool Enable { get; set; }
        public List<int> Permissions { get; set; }


    }
}