using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KELEI.Commons.AccessRPC
{
    public class NetMqResultHash
    {
        /// <summary>
        /// 存放结果的Hash
        /// </summary>
        private static Hashtable resultHash = new Hashtable();

        public static void AddHash(string key, object value)
        {
            resultHash.Add(key, value);
        }
        public static Tuple<bool, object> GetHash(string key)
        {
            //判断是否存在
            if (resultHash.ContainsKey(key))
            {
                return new Tuple<bool, object>(true, resultHash[key]);
            }
            else
            {
                return new Tuple<bool, object>(false,null);
            }
        }

        public static void DeleteHash(string key)
        {
            //判断是否存在
            if (resultHash.ContainsKey(key))
            {
                resultHash.Remove(key);
            }
            else
            {
                //不存在，不做处理
            }
        }

        public static void DeleteDicAll()
        {
            if (resultHash != null)
            {
                resultHash.Clear();
                resultHash = null;
            }
        }
    }
}
