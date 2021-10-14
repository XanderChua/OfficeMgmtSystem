using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OfficeMgmtSystem
{
    class Manager
    {
        private Dictionary<string, Admin> _adminDict;
        private Dictionary<string, User> _userDict;
        private Dictionary<string, UserReport> _userReportDict;
        private List<string> _taskList;
        public Dictionary<string, Admin> AdminDictObj
        {
            get
            {
                if (_adminDict == null)
                {
                    _adminDict = new Dictionary<string, Admin>();
                }
                return _adminDict;
            }
            set
            {
                _adminDict = value;
            }
        }
        public Dictionary<string, User> UserDictObj
        {
            get
            {
                if (_userDict == null)
                {
                    _userDict = new Dictionary<string, User>();
                }
                return _userDict;
            }
            set
            {
                _userDict = value;
            }
        }
        public Dictionary<string, UserReport> UserReportDictObj
        {
            get
            {
                if (_userReportDict == null)
                {
                    _userReportDict = new Dictionary<string, UserReport>();
                }
                return _userReportDict;
            }
            set
            {
                _userReportDict = value;
            }
        }
        public List<string> TaskObj
        {
            get
            {
                if (_taskList == null)
                {
                    _taskList = new List<string>();
                }
                return _taskList;
            }
            set
            {
                _taskList = value;
            }
        }
        public Manager()//retrieve details from text file
        {
            if (!File.Exists("Admin_Details.txt") || !File.Exists("User_Details.txt"))
            {
                Console.WriteLine("No data exist");
                return;
            }

            FileStream fs = new FileStream("Admin_Details.txt", FileMode.OpenOrCreate, FileAccess.Read);
            fs.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadLine();
            while (str != null)
            {
                var strArr = str.Split(",");
                var admin = new Admin(strArr[0], strArr[1]);
                if (!AdminDictObj.ContainsKey(strArr[0]))
                {
                    AdminDictObj.Add(strArr[0], admin);
                }
                str = sr.ReadLine();
            }
            sr.Close();
            fs.Close();

            FileStream fs1 = new FileStream("User_Details.txt", FileMode.OpenOrCreate, FileAccess.Read);
            fs1.Seek(0, SeekOrigin.Begin);
            StreamReader sr1 = new StreamReader(fs1);
            string str1 = sr1.ReadLine();
            while (!string.IsNullOrWhiteSpace(str1))
            {
                    var strArr1 = str1.Split(",");
                    var user = new User(strArr1[0], strArr1[1], strArr1[2]);
                    if (!UserDictObj.ContainsKey(strArr1[0]))
                    {
                        UserDictObj.Add(strArr1[0], user);
                    }
                    str1 = sr1.ReadLine();                   
            }
            sr1.Close();
            fs1.Close();
        }
        public void PerformOperationSuperAdmin()
        {

            bool userexit = false;
            while (!userexit)
            {
                Console.WriteLine("Select super admin option:");
                Console.WriteLine("1. Create Admin");
                Console.WriteLine("2. Create User");
                Console.WriteLine("3. Delete Admin");
                Console.WriteLine("4. Delete User");
                Console.WriteLine("5. Update and Exit");
                int userOption = Int32.Parse(Console.ReadLine());

                switch (userOption)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter new admin ID:");
                            string inputAdminID = Console.ReadLine();
                            Console.WriteLine("Enter new admin name:");
                            string inputAdminName = Console.ReadLine();
                            AdminDictObj.Add(inputAdminID, new Admin(inputAdminID, inputAdminName));
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter new user ID:");
                            string inputUserID = Console.ReadLine();
                            Console.WriteLine("Enter new user name:");
                            string inputUserName = Console.ReadLine();
                            string task = "Unassigned";
                            UserDictObj.Add(inputUserID, new User(inputUserID, inputUserName, task));
                            break;
                        }
                    case 3:
                        {
                            foreach (var admin in AdminDictObj)
                            {
                                Console.WriteLine("Admin ID: " + admin.Key + "\nAdmin Name: " + admin.Value.adminName);
                            }
                            Console.WriteLine("Enter admin ID to remove admin:");
                            string removeAdmin = Console.ReadLine();
                            if (AdminDictObj.ContainsKey(removeAdmin))
                            {
                                AdminDictObj.Remove(removeAdmin);
                                Console.WriteLine("Admin ID: " + removeAdmin + " removed.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID!");
                            }
                            break;
                        }
                    case 4:
                        {
                            foreach (var user in UserDictObj)
                            {
                                Console.WriteLine("User ID: " + user.Key + "\nUser Name: " + user.Value.userName);
                            }
                            Console.WriteLine("Enter user ID to remove user:");
                            string removeUser = Console.ReadLine();
                            if (UserDictObj.ContainsKey(removeUser))
                            {
                                UserDictObj.Remove(removeUser);
                                Console.WriteLine("User ID: " + removeUser + " removed.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID!");
                            }
                            break;
                        }
                    case 5:
                        {
                            UpdateAdminDictionary();
                            UpdateUserDictionary();
                            userexit = true;
                            break;
                        }
                }
            }
        }
        public void PerformOperationAdmin()
        {
            FileStream fs = new FileStream("Tasks.txt", FileMode.Open, FileAccess.ReadWrite);
            fs.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadLine();
            while (str != null)
            {
                TaskObj.Add(str);
                str = sr.ReadLine();
            }
            sr.Close();
            fs.Close();

            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Select admin option:");
                Console.WriteLine("1. Add new task");
                Console.WriteLine("2. Assign task");
                Console.WriteLine("3. Update and Exit");
                int userOption = Int32.Parse(Console.ReadLine());
                switch (userOption)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter new task:");
                            string inputTask = Console.ReadLine();
                            TaskObj.Add(inputTask);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Task List:");
                            foreach (var tasks in TaskObj)
                            {
                                Console.WriteLine(TaskObj.IndexOf(tasks) + 1 + ". " + tasks);
                            }
                            int inputTaskOption = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter user ID to assign:");
                            foreach (var user in UserDictObj)
                            {
                                if (user.Value.taskAssigned == "Unassigned")
                                {
                                    Console.WriteLine("User ID: " + user.Key + "\nUser Name: " + user.Value.userName);
                                }
                            }
                            string inputUserID = Console.ReadLine();


                            if (UserDictObj.ContainsKey(inputUserID))
                            {
                                foreach (var task in UserDictObj)
                                {
                                    if (task.Value.taskAssigned == "Unassigned")
                                    {
                                        UserDictObj[inputUserID].taskAssigned = TaskObj[inputTaskOption - 1];
                                    }
                                }
                                Console.WriteLine(UserDictObj[inputUserID].userName + " has been assigned to " + UserDictObj[inputUserID].taskAssigned);
                            }
                            else
                            {
                                Console.WriteLine("ID does not exist!");
                            }
                            UpdateUserTask();
                            break;
                        }
                    case 3:
                        {
                            UpdateTaskList();
                            loop = false;
                            break;
                        }
                }
            }
        }
        public void PerformOperationUser(string inputUser)
        {
            FileStream fs = new FileStream("User_Details.txt", FileMode.Open, FileAccess.ReadWrite);
            fs.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadLine();
            var strArr = str.Split(",");
            while (inputUser == strArr[0])
            {
                Console.WriteLine("Welcome " + strArr[1] + "!");
                Console.WriteLine("Your assigned task is: " + strArr[2]);
                bool loop = true;
                while (loop)
                {
                    Console.WriteLine("1. List all users assigned task");
                    Console.WriteLine("2. Write detailed report");
                    Console.WriteLine("3. Exit");
                    int inputOption = Int32.Parse(Console.ReadLine());
                    switch (inputOption)
                    {
                        case 1:
                            {
                                foreach (var user in UserDictObj)
                                {
                                    Console.WriteLine("User ID: " + user.Key + "\nUser Name: " + user.Value.userName + "\nAssigned Task:" + user.Value.taskAssigned);
                                }
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Write your report:");
                                string report = Console.ReadLine();
                                UserReportDictObj.Add(inputUser, new UserReport(strArr[0], strArr[1], strArr[2], report));
                                FileStream fsreport = new FileStream("User_Report.txt", FileMode.OpenOrCreate, FileAccess.Write);
                                StreamWriter swreport = new StreamWriter(fsreport);
                                foreach (var userreport in UserReportDictObj)
                                {
                                    swreport.WriteLine(userreport.Key + "," + userreport.Value.userNameforReport + "," + userreport.Value.taskAssignedforReport + "," + userreport.Value.userReport);
                                }
                                Console.WriteLine("Report added!");
                                swreport.Flush();
                                swreport.Close();
                                fsreport.Close();
                                break;
                            }
                        case 3:
                            {
                                loop = false;
                                break;
                            }
                    }
                }
                break;
            }
        }
        public void UpdateUserTask()
        {
            FileStream fs = new FileStream("User_Details.txt", FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var user in UserDictObj)
            {
                sw.WriteLine(user.Key + "," + user.Value.userName + "," + user.Value.taskAssigned);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public void UpdateAdminDictionary()
        {
            FileStream fs = new FileStream("Admin_Details.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var admin in AdminDictObj)
            {
                sw.WriteLine(admin.Key + "," + admin.Value.adminName);
            }
            sw.Close();
            fs.Close();
        }
        public void UpdateUserDictionary()
        {
            FileStream fs = new FileStream("User_Details.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var user in UserDictObj)
            {
                sw.WriteLine(user.Key + "," + user.Value.userName + "," + user.Value.taskAssigned);
            }
            sw.Close();
            fs.Close();
        }
        public void UpdateTaskList()
        {
            FileStream fs = new FileStream("Tasks.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var task in TaskObj)
            {
                sw.WriteLine(task);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
