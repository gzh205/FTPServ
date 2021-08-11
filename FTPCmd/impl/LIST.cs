using FTPServ.FTPCore;
using System;
using System.Globalization;
using System.IO;
using System.Net;

namespace FTPServ.FTPCmd.impl
{
    class LIST : Command
    {
        public override void operation()
        {
            string sendString = string.Empty;
            DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", true).DateTimeFormat;
            string realDir = string.Empty;
            if (param == null || param == "")
                realDir = user.currentDir;// 得到目录列表
            else
                realDir = param;
            string[] dir = Directory.GetDirectories(realDir);
            if (string.IsNullOrEmpty(param) == false)
            {
                if (Directory.Exists(user.currentDir + param))
                {
                    dir = Directory.GetDirectories(user.currentDir + param);
                }
                else
                {
                    string s = user.currentDir.TrimEnd('/');
                    user.currentDir = s.Substring(0, s.LastIndexOf("/") + 1);
                }
            }
            for (int i = 0; i < dir.Length; i++)
            {
                string folderName = Path.GetFileName(dir[i]);
                DirectoryInfo d = new DirectoryInfo(dir[i]);
                // 按下面的格式输出目录列表
                sendString += "drwxr-xr-x\t" + Dns.GetHostName() + "\t" + FTPCore.Tools.getDirSize(d).ToString() + " "//文件夹大小
                    + dateTimeFormat.GetAbbreviatedMonthName(d.CreationTime.Month)
                    + d.CreationTime.ToString(" dd yyyy") + "\t" + folderName + Environment.NewLine;
            }
            // 得到文件列表
            string[] files = Directory.GetFiles(realDir);
            if (string.IsNullOrEmpty(param) == false)
            {
                if (Directory.Exists(user.currentDir + param + "/"))
                {
                    files = Directory.GetFiles(user.currentDir + param + "/");
                }
            }
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo f = new FileInfo(files[i]);
                string fileName = Path.GetFileName(files[i]);
                // 按下面的格式输出文件列表
                sendString += "-rw-r--r--\t" + Dns.GetHostName() + "\t" + f.Length + " "
                    + dateTimeFormat.GetAbbreviatedMonthName(f.CreationTime.Month)
                    + f.CreationTime.ToString(" dd yyyy") + "\t" + fileName + Environment.NewLine;
            }
            // List命令指示获得FTP服务器上的文件列表字符串信息
            // 所以调用List命令过程，客户端接受的指示一些字符串
            // 所以isBinary是false,代表传输的是ASCII数据
            // 但是为了防止isBinary因为 设置user.isBinary = false而改变
            // 所以事先保存user.IsBinary的引用（此时为true）,方便后面下载文件
            bool isBinary = user.isBinary;
            user.isBinary = false;
            Tools.RepleyCommandToUser(user, "150 Opening ASCII data connection");
            Tools.InitDataSession(user);
            Tools.SendByUserSession(user, sendString);
            Tools.RepleyCommandToUser(user, "226 Transfer complete");
            user.isBinary = isBinary;
        }
    }
}
