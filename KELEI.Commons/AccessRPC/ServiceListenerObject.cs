using KELEI.Commons.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KELEI.Commons.AccessRPC
{
    public class ServiceListenerObject
    {
        //根据配置文件反射类
        private static Dictionary<string, object> objDiv = new Dictionary<string, object>();
        private static Dictionary<string, Tuple<string, string>> objMethod = new Dictionary<string, Tuple<string, string>>();
        /// <summary>
        /// 根据XML，反射服务接口
        /// </summary>
        public static void  Init(string objXmlPath, string objXmlNodeName)
        {
            XMLHelper objxml = new XMLHelper(objXmlPath, objXmlNodeName);
            Dictionary<string, string> nodeDic = objxml.GetNodes("id", "type");
            foreach(var item in nodeDic)
            {
                var obj = GetObjec(item.Value);
                objDiv.Add(item.Key, obj);
                //解析obj中的方法，返回注册的服务接口MessageListener
                Dictionary<string, string> methodList = GetObjectMethod(obj);
                foreach(var mo in methodList)
                {
                    objMethod.Add(mo.Key, new Tuple<string, string>(item.Key, mo.Value));
                }
            }
        }

        public static object ExceMethod(BaseMessage mess)
        {
            string subjectID = mess.Subject;
            Type[] params_type = new Type[1];
            var method = objMethod[subjectID];
            //根据objectid返回object
            object obj = objDiv[method.Item1];
            Object[] parametors = new Object[] { mess };
            //执行方法返回值
            var result = obj.GetType().GetMethod(method.Item2).Invoke(obj, parametors);
            if (result == null) return null;
            return result;
        }

        public static void DelectDic()
        {
            if(objDiv!=null)
            {
                objDiv.Clear();
                objDiv = null;
            }
            if(objMethod!=null)
            {
                objMethod.Clear();
                objMethod = null;
            }
        }
        private static object GetObjec(string typeName)
        {
            try
            {
                Type o = Type.GetType(typeName);//加载类型
                object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                return obj;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 返回object中注册有MessageListener特性的方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Dictionary<string,string> GetObjectMethod(object obj)
        {
            //遍历类中的所有方法，返回特性
            Dictionary<string, string> methodList = new Dictionary<string, string>();
            MethodInfo[] methods = obj.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                //判断是否存在MessageListenerAttribute特性
                object[] objAttrs = method.GetCustomAttributes(typeof(MessageListenerAttribute), true);
                if (objAttrs.Length > 0)
                {
                    var attrib = (MessageListenerAttribute)objAttrs[0];
                    var id = attrib.MessageSubject;
                    var name = method.Name;
                    methodList.Add(id, name);
                }
            }
            return methodList;
        }
    }
}