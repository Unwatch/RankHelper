using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RankHelper
{
    class FileUtils
    {
        /// <summary>
        /// 清空指定的文件夹，但不删除文件夹
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFolder(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    try
                    {
                        FileInfo fi = new FileInfo(d);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                            fi.Attributes = FileAttributes.Normal;
                        File.Delete(d);//直接删除其中的文件 
                    }
                    catch
                    {

                    }
                }
                else
                {
                    try
                    {
                        DirectoryInfo d1 = new DirectoryInfo(d);
                        if (d1.GetFiles().Length != 0)
                        {
                            DeleteFolder(d1.FullName);////递归删除子文件夹
                        }
                        Directory.Delete(d);
                    }
                    catch
                    {

                    }
                }
            }
        }
        /// <summary>
        /// 删除文件夹及其内容
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFolder1(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件 
                }
                else
                    DeleteFolder(d);////递归删除子文件夹
                Directory.Delete(d);
            }
        }
    }

}
