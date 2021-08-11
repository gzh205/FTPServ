using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.SysCommand.impl
{
    class User : Command
    {
        public override string discription => "打开用户编辑器";

        public override void operation()
        {
            UserInfo inf = new UserInfo();
            inf.ShowDialog();
        }
    }
}
