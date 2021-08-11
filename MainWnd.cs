using FTPServ.CmdIO;
using System;
using System.Threading;
using System.Windows.Forms;

namespace FTPServ
{
    public partial class MainWnd : Form
    {
        public MainWnd()
        {
            InitializeComponent();
        }

        private void txtConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cmd = Console.ReadLine().Trim(' ','\r','\n');
                string[] cc = cmd.Split(new char[]{ ' '},2);
                if (cc.Length == 1)
                {
                    new Thread(()=> { SysCommand.Command.call(cc[0], ""); }).Start();
                }
                else if (cc.Length == 2)
                {
                    new Thread(() => { SysCommand.Command.call(cc[0], cc[1]); }).Start();          
                }
                else
                    Console.WriteLine("输入错误");
            }
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
            new TextConsoleWriter(this.txtConsole);
            new TextConsoleReader(this.txtConsole);
            Console.WriteLine("加载完毕");
        }
    }
}
