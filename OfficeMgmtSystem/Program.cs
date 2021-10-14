using System;
using System.Collections.Generic;
using System.IO;
//need to design a office mgmt. system , here  it consists of a three layred structure .means the super admin , admin , and the user.
//The super admin has all the access to delete the user , create the user and all . But the admin can only have access to give task to the users.
//User just have only on option that is to Log in and see the task he assigned and submit the detailed report of that particular work. Use file handling to store all the data

namespace OfficeMgmtSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            Console.WriteLine("--Office Management System--");
            Console.WriteLine("Enter ID to login:");
            string inputID = Console.ReadLine();
            if (inputID == "superadmin")
            {
                manager.PerformOperationSuperAdmin();
            }
            else if (manager.AdminDictObj.ContainsKey(inputID))
            {
                manager.PerformOperationAdmin();
            }
            else if (manager.UserDictObj.ContainsKey(inputID))
            {
                manager.PerformOperationUser(inputID);
            }
            else
            {
                Console.WriteLine("Invalid ID!");
            }
        }
    }
}
