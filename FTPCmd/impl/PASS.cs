using FTPServ.FTPCore;
using System.IO;

namespace FTPServ.FTPCmd.impl
{
    class PASS : Command
    {
        public override void operation()
        {
            string sendString = string.Empty;
            Config.User u = null;
            if (Config.User.users.TryGetValue(user.userName, out u))
            {
                if (u.password == param)
                {        
                    // 2表示登录成功
                    user.loginOK = 2;
                    user.userName = u.userName;
                    if (new DirectoryInfo(u.workDir).Exists)
                    {
                        user.workDir = u.workDir;
                        user.currentDir = user.workDir;
                        sendString = "230 User logged in success";
                    }
                    else
                    {
                        sendString = "530 Password incorrect.";
                    }
                }
                else
                {
                    sendString = "530 Password incorrect.";
                }
            }
            else
            {
                sendString = "530 User name or password incorrect.";
            }
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
