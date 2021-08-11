using FTPServ.FTPCore;

namespace FTPServ.FTPCmd.impl
{
    class NotImpl : Command
    {
        public override void operation()
        {
            Tools.RepleyCommandToUser(user, "502 command is not implemented.");
        }
    }
}
