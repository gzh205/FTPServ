using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace FTPServ.FTPCore
{
    public class FTPListener
    {
        public TcpListener myTcpListener;
        public Thread listenThread;//FTP控制端口
        public FTPListener()
        {
            if (myTcpListener == null)
            {
                listenThread = new Thread(ListenClientConnect);
                listenThread.IsBackground = true;
                listenThread.Start();//启动监听线程
            }
        }
        // 监听端口，处理客户端连接
        private void ListenClientConnect()
        {
            myTcpListener = new TcpListener(IPAddress.Parse(Config.ConfigManager.config_string["ip_addr"]), Convert.ToInt32(Config.ConfigManager.config_string["port"]));
            // 开始监听传入的请求
            myTcpListener.Start();
            //启动成功
            while (true)
            {
                try
                {
                    // 接收连接请求
                    TcpClient tcpClient = myTcpListener.AcceptTcpClient();
                    //建立Ftp连接
                    User user = new User();
                    user.commandSession = new UserSeesion(tcpClient);
                    user.workDir = Config.ConfigManager.config_string["root_dir"];
                    Thread t = new Thread(UserProcessing);
                    t.IsBackground = true;
                    t.Start(user);
                }
                catch
                {
                    break;
                }
            }
        }

        // 处理客户端用户请求
        private void UserProcessing(object obj)
        {
            User user = (User)obj;
            string sendString = "220 FTP Server v1.0";
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
            while (true)
            {
                string receiveString = null;
                try
                {
                    // 读取客户端发来的请求信息
                    receiveString = user.commandSession.streamReader.ReadLine();
                }
                catch (Exception ex)
                {
                    if (user.commandSession.tcpClient.Connected == false)
                    {
                        Console.WriteLine(string.Format("客户端({0}断开连接！)", user.commandSession.tcpClient.Client.RemoteEndPoint));
                    }
                    else
                    {
                        Console.WriteLine("接收命令失败！" + ex.Message);
                    }

                    break;
                }

                if (receiveString == null)
                {
                    Console.WriteLine("接收字符串为null,结束线程！");
                    break;
                }

                Console.WriteLine(string.Format("来自{0}：[{1}]", user.commandSession.tcpClient.Client.RemoteEndPoint, receiveString));

                // 分解客户端发来的控制信息中的命令和参数
                string command = receiveString;
                string param = string.Empty;
                int index = receiveString.IndexOf(' ');
                if (index != -1)
                {
                    command = receiveString.Substring(0, index).ToUpper();
                    param = receiveString.Substring(command.Length).Trim();
                }

                // 处理不需登录即可响应的命令（这里只处理QUIT）
                if (command == "QUIT")
                {
                    // 关闭TCP连接并释放与其关联的所有资源
                    user.commandSession.Close();
                    return;
                }
                else
                {
                    FTPCmd.Command.call(command, user, param);
                    /*
                    switch (user.loginOK)
                    {
                        // 等待用户输入用户名：
                        case 0:
                            FTPCmd.Command.call("USER", user, param);
                            break;

                        // 等待用户输入密码
                        case 1:
                            FTPCmd.Command.call("PASS", user, param);
                            break;

                        // 用户名和密码验证正确后登陆
                        case 2:
                            FTPCmd.Command.call(command, user, param);
                            break;
                    }*/
                }
            }
        }
    }
}
