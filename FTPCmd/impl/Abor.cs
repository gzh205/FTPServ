using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    [Bind(classname=typeof(NotImpl))]
    class Abor : Command
    {
        /// <summary>
        /// (ABORT)此命令使服务器终止前一个FTP服务命令以及任何相关数据传输。
        /// 返回码
        /// 225, 226
        /// 500, 501, 502, 421
        /// </summary>
        public override void operation()
        {
            throw new NotImplementedException();
        }
    }
}
