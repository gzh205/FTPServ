using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.SysCommand.impl
{
    class Stop : Command
    {
        public override string discription => "|关闭服务器";

        public override void operation()
        {
            if (Start.inst == null)
            {
                Console.WriteLine("服务器已停止，无需再次停止");
            }
            else
            {
                Start.inst.myTcpListener.Stop();
                Start.inst = null;
                Console.WriteLine("已停止服务器");
            }
        }
    }
}
