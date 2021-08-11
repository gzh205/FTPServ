using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    class OPTS : Command
    {
        public override void operation()
        {
            string sendString = string.Empty;
            string[] arr = param.Split(' ');
            switch (arr[0])
            {
                case "UTF8":
                    {
                        if (arr[1] == "ON")
                        {
                            sendString = "502 command has not been implemented yet";
                        }
                        else if (arr[1] == "OFF")
                        {
                            sendString = "200 UTF8 mode disabled";
                        }
                        else
                        {
                            sendString = "500 command error";
                        }
                    }
                    break;
                default:
                    sendString = "502 command has not been implemented yet";
                    break;
            }
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
