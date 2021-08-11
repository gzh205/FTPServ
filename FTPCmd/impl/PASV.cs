using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    class PASV : Command
    {
        public override void operation()
        {
            string sendString = null;
            string ip = Config.ConfigManager.config_string["ip_addr"];
            IPAddress localip = IPAddress.Parse(ip);
            // 被动模式，即服务器接收客户端的连接请求
            // 被动模式下FTP服务器使用随机生成的端口进行传输数据
            // 而主动模式下FTP服务器使用端口20进行数据传输
            Random random = new Random();
            int random1, random2;
            int port;
            while (true)
            {
                // 随机生成一个端口进行数据传输
                random1 = random.Next(5, 200);
                random2 = random.Next(0, 200);
                // 生成的端口号控制>1024的随机端口
                // 下面这个运算算法只是为了得到一个大于1024的端口值
                port = random1 << 8 | random2;
                try
                {
                    user.dataListener = new TcpListener(localip, port);
                    Console.WriteLine("TCP 数据连接已打开（被动模式）--" + localip.ToString() + "：" + port);
                }
                catch
                {
                    continue;
                }
                user.isPassive = true;
                string temp = localip.ToString().Replace('.', ',');
                // 必须把端口号IP地址告诉客户端，客户端接收到响应命令后，
                // 再通过新的端口连接服务器的端口P，然后进行文件数据传输
                sendString = "227 Entering Passive Mode(" + temp + "," + random1 + "," + random2 + ")";
                user.dataListener.Start();
                break;
            }
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
