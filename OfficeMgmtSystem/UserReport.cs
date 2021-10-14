using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMgmtSystem
{
    class UserReport
    {
        public string userIDforReport { get; private set; }
        public string userNameforReport { get; private set; }
        public string taskAssignedforReport { get; private set; }
        public string userReport { get; set; }
        public UserReport(string id, string name, string task, string report)
        {
            userIDforReport = id;
            userNameforReport = name;
            taskAssignedforReport = task;
            userReport = report;
        }
    }
}
