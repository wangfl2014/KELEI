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
        public virtual Base_Employee GetUser(BaseMessage msg)
        {
            string errorMsg = "";
            Base_Employee res = null;
            string userID = string.Empty;
            try
            {
                //解析传入body
                ReceiveMessageHandle rmHandle = new ReceiveMessageHandle(msg);
                rmHandle.GetParameters<string>(out userID);
                res = userServer.GetUser(userID);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                res = null;
            }
            return res;
        }

        [MessageListener("6b90f8b9-69df-40ec-900c-5793d6d1d351-GetUsers")]
        public virtual Tuple<long, List<Base_Employee>> GetUsers(BaseMessage msg)
        {
            string errorMsg = "";
            Tuple<long, List<Base_Employee>> res = null;
            long startRow = 0;
            long endRow = 0;
            try
            {
                //解析传入body
                ReceiveMessageHandle rmHandle = new ReceiveMessageHandle(msg);
                rmHandle.GetParameters<long,long>(out startRow,out endRow);
                res = userServer.GetUsers(startRow, endRow);
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
