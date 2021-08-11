using FTPServ.FTPCore;
using System.IO;

namespace FTPServ.FTPCmd.impl
{
    class CWD : Command
    {
        public override void operation()
        {
            string sendString = string.Empty;
            try
            {
                string dir = user.workDir.TrimEnd('/') + param;
                // 是否为当前目录的子目录，且不包含父目录名称
                if (Directory.Exists(dir))
                {
                    user.currentDir = dir;
                    sendString = "250 Directory changed to '" + dir + "' successfully";
                }
                else
                {
                    sendString = "550 Directory '" + dir + "' does not exist";
                }
            }
            catch
            {
                sendString = "502 Directory changed unsuccessfully";
            }
            Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
