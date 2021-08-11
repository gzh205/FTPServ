using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    /// <summary>
    /// 获取操作系统信息
    /// </summary>
    class SYST : Command
    {
        public override void operation()
        {
            string sendString = "211 " + Environment.OSVersion.ServicePack;
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
