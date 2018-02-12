using System;
using System.IO;
using System.Text;

namespace KELEI.Commons.Helper
{
    public static class FileHelper
    {
        /// <summary>
        /// 获取当前应用程序下文件的绝对路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ApplicationPath(string fileName)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(path, fileName);
        }

        /// <summary>
        /// 读取文本文件内容
        /// </summary>
        /// <param name="filePathName">文件路径名称</param>
        /// <param name="fileType">文件类型utf-8，utf-16，GB2312</param>
        /// <returns></returns>
        public static string ReadTextFile(string filePathName, string fileType = "utf-8")
        {
            //判断目录是否存在
            if (!File.Exists(filePathName))
            {
                return null;
            }
            Encoding fileEncoding = Encoding.GetEncoding(fileType);
            StreamReader sr = new StreamReader(filePathName, fileEncoding);
            String line;
            StringBuilder json = new StringBuilder();
            while ((line = sr.ReadLine()) != null)
            {
                json.Append(line.ToString());
            }
            return json.ToString();
        }
        /// <summary>
        ///  创建写文本文件并写入内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileType">字符编码类型utf-8，utf-16，GB2312</param>
        /// <param name="strContent">写入内容</param>
        /// <returns>是否操作成功</returns>
        public static bool CreateTxtFile(string filePath, string fileName, string fileType, string strContent)
        {
            Encoding fileEncoding = Encoding.GetEncoding(fileType);
            //判断目录是否存在
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            FileStream fs = File.Create(filePath + "/" + fileName);
            try
            {
                Byte[] bContent = fileEncoding.GetBytes(strContent);
                fs.Write(bContent, 0, bContent.Length);
                fs.Close();
                fs = null;
                return true;
            }
            catch
            {

                fs.Close();
                fs = null;
                return false;
            }


        }

        /// <summary>
        /// 递归拷贝所有子目录。
        /// </summary>
        /// <param name="sPath">源目录</param>
        /// <param name="dPath">目的目录</param>
        public static void CopyDirectory(string sPath, string dPath)
        {
            //string[] directories = System.IO.Directory.GetDirectories(sPath);
            if (!System.IO.Directory.Exists(dPath))
            {
                System.IO.Directory.CreateDirectory(dPath);
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sPath);
            System.IO.DirectoryInfo[] dirs = dir.GetDirectories();
            CopyFile(dir, dPath);
            if (dirs.Length > 0)
            {
                foreach (System.IO.DirectoryInfo temDirectoryInfo in dirs)
                {
                    string sourceDirectoryFullName = temDirectoryInfo.FullName;
                    string destDirectoryFullName = sourceDirectoryFullName.Replace(sPath, dPath);
                    if (!System.IO.Directory.Exists(destDirectoryFullName))
                    {
                        System.IO.Directory.CreateDirectory(destDirectoryFullName);
                    }
                    CopyFile(temDirectoryInfo, destDirectoryFullName);
                    CopyDirectory(sourceDirectoryFullName, destDirectoryFullName);
                }
            }

        }

        /// <summary>
        /// 拷贝目录下的所有文件到目的目录。
        /// </summary>
        /// <param name="path">源路径</param>
        /// <param name="desPath">目的路径</param>
        private static void CopyFile(System.IO.DirectoryInfo path, string desPath)
        {
            string sourcePath = path.FullName;
            System.IO.FileInfo[] files = path.GetFiles();
            foreach (System.IO.FileInfo file in files)
            {
                string sourceFileFullName = file.FullName;
                string destFileFullName = sourceFileFullName.Replace(sourcePath, desPath);
                try
                {
                    file.CopyTo(destFileFullName, true);
                }
                catch
                {
                }
            }
        }
    }
}