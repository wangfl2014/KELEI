using KELEI.Commons.AccessRPC;
using KELEI.PM.DBService.Servers;
using KELEI.PM.Entity;
using System;
using System.Collections.Generic;

namespace KELEI.PM.DBService.Listener
{
    public class Users_Listener
    {
        User_Service userServer = new User_Service();

        [MessageListener("6b90f8b9-69df-40ec-900c-5793d6d1d351-GetUser")]
        public virtual byte[] GetUser(BaseMessage msg)
        {
            string errorMsg = "";
            byte[] res = null;
            string userID = string.Empty;
            try
            {
                //解析传入body
                ReceiveMessageHandle rmHandle = new ReceiveMessageHandle(msg);
                rmHandle.GetParameters<string>(out userID);
                var user = userServer.GetUser(userID);
                res = ProtoSerialize.Serialize<Base_Employee>(user);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                res = null;
            }
            return res;
        }

        [MessageListener("6b90f8b9-69df-40ec-900c-5793d6d1d351-GetUsers")]
        public virtual byte[] GetUsers(BaseMessage msg)
        {
            string errorMsg = "";
            byte[] res = null;
            int startRow = 0;
            int endRow = 0;
            try
            {
                //解析传入body
                ReceiveMessageHandle rmHandle = new ReceiveMessageHandle(msg);
                rmHandle.GetParameters<int,int>(out startRow,out endRow);
                var users = userServer.GetUsers(startRow, endRow);
                var resObj = new Base_EmployeeList()
                {
                    CountRow= users.Item1,
                    Base_Employees=users.Item2
                };
                res = ProtoSerialize.Serialize<Base_EmployeeList>(resObj);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                res = null;
            }
            return res;
        }

        [MessageListener("6b90f8b9-69df-40ec-900c-5793d6d1d351-InsertUser")]
        public virtual byte[] InsertUser(BaseMessage msg)
        {
            string errorMsg = "";
            byte[] res = null;
            Base_Employee user;
            try
            {
                //解析传入body
                ReceiveMessageHandle rmHandle = new ReceiveMessageHandle(msg);
                rmHandle.GetParameters<Base_Employee>(out user);
                var users = userServer.InsertUser(user)?.Item1;
                res = ProtoSerialize.Serialize<bool>(users ?? false);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                res = null;
            }
            return res;
        }
    }
}
