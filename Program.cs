using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPServ
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            init();
            Application.Run(new MainWnd());
        }
        public static void init()
        {
            SysCommand.Command.init();//加载软件自带的命令
            FTPCmd.Command.init();//加载FTPCmd.impl下的FTP命令
            Config.ConfigManager.ReadConfig();//读配置文件
            Config.User.ReadUsers();//读取数据库中的用户组
        }
    }
}
