using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    
    public class CallActionsEntity
    {
        public DateTime Time_Stamp { get; set; }
        public string UserName { get; set; }
        public DateTime CallDate { get; set; }
        public string Call_CLI { get; set; }
        public string Call_DDI { get; set; }
        string extension;
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        string first_name;
        public string FirstName
        {
            get { return first_name; }
            set { first_name = value; }
        }
        string last_name;
        public string LastName
        {
            get { return last_name; }
            set { last_name = value; }
        }
       
        public string ExtensionWithName
        {
            get
            {
                if (extension == "" && FirstName == "" && LastName == "")
                {
                    return "Unknown";
                }
                else if (extension != "" && FirstName == "" && LastName == "")
                {
                    return extension;
                }
                else if (extension == "" && (FirstName != "" || LastName != ""))
                {
                    return FirstName + " " + LastName;
                }
                else
                {
                    return extension + " (" + FirstName + " " + LastName + ")";
                }
            }

        }

        string action;
        public string Action
        {
            get { return action; }
            set { action = value; }
        }
    }

    public class CallNotesEntity
    {
        public string NotesTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CallDate { get; set; }
        public string Call_CLI { get; set; }
        public string Call_DDI { get; set; }
        public string Note { get; set; }
        public int CallDetailId { get; set; }      

        public string FullName
        {
            get
            {
                try
                {
                    return FirstName + " " + LastName;
                }
                catch(Exception ex)
                {

                }
                return "";
            }
        }
    }
}