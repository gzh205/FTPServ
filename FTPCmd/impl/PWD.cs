using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    class PWD : Command
    {
        public override void operation()
        {
            string sendString = string.Empty;
            user.currentDir = user.workDir;
            sendString = "250 '" + user.currentDir + "' is the current directory";
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
