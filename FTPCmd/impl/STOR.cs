using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    class STOR : Command
    {
        public override void operation()
        {
            string sendString = string.Empty;
            // 上传的文件全名
            string path = user.currentDir + param;
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            // 发送150到用户，表示服务器状态良好
            if (user.isBinary)
            {
                sendString = "150 Opening BINARY mode data connection for upload";
            }
            else
            {
                sendString = "150 Opeing ASCII mode data connection for upload";
            }
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
            FTPCore.Tools.InitDataSession(user);
            FTPCore.Tools.ReadFileByUserSession(user, fs);
            FTPCore.Tools.RepleyCommandToUser(user, "226 Transfer complete");
        }
    }
}
