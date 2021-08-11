using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.SysCommand.impl
{
    class Help : Command
    {
        public override string discription => "|显示所有的命令";

        public override void operation()
        {
            foreach(string str in Command.commands.Keys)
            {
                Console.WriteLine(str+" "+Command.commands[str].discription);
            }
        }
    }
}
