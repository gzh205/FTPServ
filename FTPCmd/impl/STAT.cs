using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    /// <summary>
    /// 获取当前程序和目录信息
    /// </summary>
    class STAT : Command
    {
        public override void operation()
        {
            string sendString = string.Empty;
            if (user.loginOK == 2)
            {
                sendString = "212 Directory status";
            }
            else
            {
                sendString = "211 System status, or system help reply";
            }
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
            //sendString = "213 File status";
        }
    }
}
