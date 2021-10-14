using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMgmtSystem
{
    class Admin
    {
        public string adminID { get; private set; }
        public string adminName { get; private set; }
        public Admin(string id, string name)
        {
            adminID = id;
            adminName = name;
        }
    }

}
