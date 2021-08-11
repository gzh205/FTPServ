using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    class HELP : Command
    {
        public override void operation()
        {
            string sendString = "214 那么多命令根本实现不过来，帮助也懒得写反正，只有我一个人用";
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
