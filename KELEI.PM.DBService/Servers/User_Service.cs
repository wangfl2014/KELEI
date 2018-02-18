using KELEI.Commons.Helper;
using KELEI.PM.DBService.Repository;
using KELEI.PM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KELEI.PM.DBService.Servers
{
    public class User_Service
    {
        private BaseRepository<Base_Employee> users = new Base_EmployeeDal();

        public Tuple<long, List<Base_Employee>> GetUsers(long startrow=0,long endrow=0)
        {
            return users.repository.GetListByDic(null,null, startrow, endrow);
        }

        public Base_Employee GetUser(string userID)
        {
            return users.repository.GetByKey(new { UserMail = userID });
        }

        public Tuple<bool, string> InsertUser(Base_Employee user)
        {
            return users.repository.InsertEntitile(user);
        }
    }
}
