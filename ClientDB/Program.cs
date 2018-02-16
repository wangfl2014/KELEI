using System;
using KELEI.Commons.AccessRPC;
using KELEI.PM.Entity;

namespace ClientDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            NetMqPush push = new NetMqPush("127.0.0.1:5557");
            NetMqPull pull = new NetMqPull("127.0.0.1:5558");
            //string userID = "bj1@kmdev.com.cn";
            //var sendMess = new SendMessageHandle("6b90f8b9-69df-40ec-900c-5793d6d1d351-GetUser", userID).GetMessage;
            //var user = push.Send<Base_Employee>(sendMess);
            //Console.WriteLine(user.UserMail + ":" + user.UserName);

            //var sendMess1 = new SendMessageHandle("6b90f8b9-69df-40ec-900c-5793d6d1d351-GetUsers").GetMessage;
            //var users = push.Send<Base_EmployeeList>(sendMess1);
            //Console.WriteLine(users.CountRow);

            var sendMess2 = new SendMessageHandle("6b90f8b9-69df-40ec-900c-5793d6d1d351-GetUsers",10,20).GetMessage;
            var users2= push.Send<Base_EmployeeList>(sendMess2);
            Console.WriteLine(users2.CountRow+":"+ users2.Base_Employees.Count);
            Console.ReadLine();
        }
    }
}
