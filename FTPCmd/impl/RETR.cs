using System;
using System.IO;

namespace FTPServ.FTPCmd.impl
{
    class RETR : Command
    {
        public override void operation()
        {
            string sendString = "";
            // 下载的文件全名
            string path = user.currentDir + param;
            FileStream filestream = new FileStream(path, FileMode.Open, FileAccess.Read);
            // 发送150到用户，表示服务器文件状态良好，将要打开数据连接传输文件
            if (user.isBinary)
            {
                sendString = "150 Opening BINARY mode data connection for download";
            }
            else
            {
                sendString = "150 Opening ASCII mode data connection for download";
            }
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
            FTPCore.Tools.InitDataSession(user);
            FTPCore.Tools.SendFileByUserSession(user, filestream);
            FTPCore.Tools.RepleyCommandToUser(user, "226 Transfer complete");
        }
    }
}
