using System;

namespace FTPServ.FTPCmd.impl
{
    /// <summary>
    /// 该类使用了绑定功能，将自己的实现绑定到了LIST.operation()，因此该类的operation永远不会被调用。
    /// </summary>
    [Bind(classname = typeof(LIST))]
    class NLIST : Command
    {
        public override void operation()
        {
            throw new NotImplementedException();
        }
    }
}