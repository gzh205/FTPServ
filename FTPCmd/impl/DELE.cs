using System;
using System.IO;

namespace FTPServ.FTPCmd.impl
{
    class DELE : Command
    {
        public override void operation()
        {
            string sendString = "";
            // 删除的文件全名
            string path = user.currentDir + param;
            Console.WriteLine("正在删除文件" + param + "...");
            File.Delete(path);
            Console.WriteLine("删除成功");
            sendString = "250 File " + param + " has been deleted.";
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
