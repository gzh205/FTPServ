using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    class NOOP : Command
    {
        public override void operation()
        {
            string sendString = "200 command successfuly";
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
