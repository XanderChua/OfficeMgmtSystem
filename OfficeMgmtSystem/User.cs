using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMgmtSystem
{
    class User
    {
        public string userID { get; private set; }
        public string userName { get; private set; }
        public string taskAssigned { get; set; }
        public User(string id, string name, string task)
        {
            userID = id;
            userName = name;
            taskAssigned = task;
        }
    }
}
