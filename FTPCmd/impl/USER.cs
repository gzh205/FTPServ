using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    class USER : Command
    {
        public override void operation()
        {
            string sendString = string.Empty;
            if (Config.User.users.ContainsKey(param))
                sendString = "331 USER command OK, password required.";
            else
                sendString = "331 USER command OK, but user name error";
            user.userName = param;
            // 设置loginOk=1为了确保后面紧接的要求输入密码
            // 1表示已接收到用户名，等到接收密码
            user.loginOK = 1;
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
