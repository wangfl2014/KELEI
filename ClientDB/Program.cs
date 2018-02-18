using System;
using KELEI.Commons.AccessRPC;
using KELEI.PM.Entity;
using Newtonsoft.Json;

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

            var sendMess1 = new SendMessageHandle("6b90f8b9-69df-40ec-900c-5793d6d1d351-GetUsers").GetMessage;
            var users = push.Send<Base_EmployeeList>(sendMess1);
            var userJson = JsonConvert.SerializeObject(users);

            Console.WriteLine(users.CountRow);
            Console.WriteLine(users.CountRow);

            var sendMess2 = new SendMessageHandle("6b90f8b9-69df-40ec-900c-5793d6d1d351-GetUsers", 10, 20).GetMessage;
            var users2 = push.Send<Base_EmployeeList>(sendMess2);
            Console.WriteLine(users2.CountRow + ":" + users2.Base_Employees.Count);

            Base_Employee user = new Base_Employee()
            {
                UserMail = "test1@qq.com",
                UserName = "qq"
            };
            var sendMess3 = new SendMessageHandle("6b90f8b9-69df-40ec-900c-5793d6d1d351-InsertUser", user).GetMessage;
            var users3 = push.Send<bool>(sendMess3);
            Console.WriteLine(users3);
            Console.ReadLine();
        }
    }
}
