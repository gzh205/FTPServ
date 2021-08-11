using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    class TYPE : Command
    {
        public override void operation()
        {
            string sendstring = "";
            if (param == "I")
            {
                // 二进制
                user.isBinary = true;
                sendstring = "220 Type set to I(Binary)";
            }
            else
            {
                // ASCII方式
                user.isBinary = false;
                sendstring = "220 Type set to A(ASCII)";//330
            }
            FTPCore.Tools.RepleyCommandToUser(user, sendstring);
        }
    }
}
