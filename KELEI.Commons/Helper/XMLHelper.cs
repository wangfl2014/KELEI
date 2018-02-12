using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace KELEI.Commons.Helper
{
    public class XMLHelper
    {
        private string appsttingUrl = string.Empty;
        private XmlDocument doc = null;
        private XmlNodeList nodeList = null;
        private string xmlSelectNodes = string.Empty;

        public XMLHelper(string pathXML, string selectNodes)
        {
            if (nodeList == null)
            {
                xmlSelectNodes = selectNodes;
                appsttingUrl = pathXML;
                nodeList = LoadDataList(pathXML);
            }
        }

        /// <summary>
        /// 解析XML配置文件 返回XmlNodeList 对象
        /// </summary>
        /// <param name="xmlPath">配置文件路径</param>
        /// <returns>配置信息的列表</returns>
        private XmlNodeList LoadDataList(string xmlPath)
        {
            if (string.IsNullOrEmpty(xmlPath)) return null;
            string path = string.Empty;
            if(HttpContext.Current!=null)
            {
                path = HttpContext.Current.Server.MapPath(xmlPath);
            }
            else
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + xmlPath;
            }
            if (!System.IO.File.Exists(path))
                return null;
            doc = new XmlDocument();
            doc.Load(path);
            nodeList = doc.DocumentElement.SelectNodes(xmlSelectNodes); //Student1.xml文件里面的
            return nodeList;
        }

        /// <summary>
        /// 返回xml类型为key-value类型的
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>返回字典</returns>
        public Dictionary<string,string> GetNodes(string key, string value)
        {
            if (nodeList == null)
            {
                return null;
            }
            Dictionary<string, string> nodes = new Dictionary<string, string>();
            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;
                nodes.Add(node.Attributes[key].Value,node.Attributes[value].Value);
            }
            return nodes;
        }
    }
}