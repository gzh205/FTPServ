using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    [Bind(classname = typeof(NotImpl))]
    class ACCT : Command
    {
        public override void operation()
        {
            throw new NotImplementedException();
        }
    }
}
