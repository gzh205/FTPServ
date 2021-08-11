using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd.impl
{
    class PORT : Command
    {
        public override void operation()
        {
            // 主动模式时，客户端必须告知服务器接收数据的端口号，PORT 命令格式为：PORT address
            // address参数的格式为i1、i2、i3、i4、p1、p2,其中i1、i2、i3、i4表示IP地址
            // 下面通过.字符串来组合这四个参数得到IP地址
            // p1、p2表示端口号，下面通过int.Parse(temp[4]) << 8) | int.Parse(temp[5]
            // 这个算法来获得一个大于1024的端口来发送给服务器
            string sendString = string.Empty;
            string[] temp = param.Split(',');
            string ipString = "" + temp[0] + "." + temp[1] + "." + temp[2] + "." + temp[3];
            // 客户端发出PORT命令把客户端的IP地址和随机的端口告诉服务器
            int portNum = (int.Parse(temp[4]) << 8) | int.Parse(temp[5]);
            user.remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipString), portNum);
            sendString = "200 PORT command successful.";
            // 服务器以接受到的客户端IP地址和端口为目标发起主动连接请求
            // 服务器根据客户端发送过来的IP地址和端口主动发起与客户端建立连接
            FTPCore.Tools.RepleyCommandToUser(user, sendString);
        }
    }
}
