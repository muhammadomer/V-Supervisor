using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public class ExtensionsEntity
    {
        public int Id { get; set; }
        public string Extension { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
        public int[] DepartmentIDs { get; set; }
        public bool IsDashboard { get; set; }
        public bool IsConsole { get; set; }
        public List<Department> Departments { get; set; }
    }

    public class ContactEntity
    {
        public int Id { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string PhoneWork { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneMobile { get; set; }       
    }
}