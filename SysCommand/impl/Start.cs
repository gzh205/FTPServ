using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.SysCommand.impl
{
    class Start : Command
    {
        public static FTPCore.FTPListener inst;
        public override string discription => "|打开FTP服务器";

        public override void operation()
        {
            if (inst == null)
            {
                inst = new FTPCore.FTPListener();
                Console.WriteLine("服务器启动完毕");
            }
            else
                Console.WriteLine("服务器已启动，无需再次启动");
        }
    }
}
