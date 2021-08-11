using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCore
{
    public class Tools
    {
        // 想客户端返回响应码
        public static void RepleyCommandToUser(User user, string str)
        {
            try
            {
                user.commandSession.streamWriter.WriteLine(str);
                Console.WriteLine(string.Format("向客户端（{0}）发送[{1}]", user.commandSession.tcpClient.Client.RemoteEndPoint, str));
            }
            catch
            {
                Console.WriteLine(string.Format("向客户端（{0}）发送信息失败", user.commandSession.tcpClient.Client.RemoteEndPoint));
            }
        }
        // 初始化数据连接
        public static void InitDataSession(User user)
        {
            TcpClient client = null;
            if (user.isPassive)
            {
                Console.WriteLine("采用被动模式返回LIST目录和文件列表");
                client = user.dataListener.AcceptTcpClient();
            }
            else
            {
                Console.WriteLine("采用主动模式向用户发送LIST目录和文件列表");
                client = new TcpClient();
                client.Connect(user.remoteEndPoint);
            }

            user.dataSession = new UserSeesion(client);
        }

        // 使用数据连接发送字符串
        public static void SendByUserSession(User user, string sendString)
        {
            Console.WriteLine("向用户发送(字符串信息)：[" + sendString + "]");
            try
            {
                user.dataSession.streamWriter.WriteLine(sendString);
                Console.WriteLine("发送完毕");
            }
            finally
            {
                user.dataSession.Close();
            }
        }

        // 使用数据连接发送文件流（客户端发送下载文件命令）
        public static void SendFileByUserSession(User user, FileStream fs)
        {
            Console.WriteLine("向用户发送(文件流)：[...");
            try
            {
                if (user.isBinary)
                {
                    byte[] bytes = new byte[1024];
                    BinaryReader binaryReader = new BinaryReader(fs);
                    int count = binaryReader.Read(bytes, 0, bytes.Length);
                    while (count > 0)
                    {
                        user.dataSession.binaryWriter.Write(bytes, 0, count);
                        user.dataSession.binaryWriter.Flush();
                        count = binaryReader.Read(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    StreamReader streamReader = new StreamReader(fs);
                    while (streamReader.Peek() > -1)
                    {
                        user.dataSession.streamWriter.WriteLine(streamReader.ReadLine());
                    }
                }

                Console.WriteLine("...]发送完毕！");
            }
            finally
            {
                user.dataSession.Close();
                fs.Close();
            }
        }

        // 使用数据连接接收文件流(客户端发送上传文件功能)
        public static void ReadFileByUserSession(User user, FileStream fs)
        {
            Console.WriteLine("接收用户上传数据（文件流）：[...");
            try
            {
                if (user.isBinary)
                {
                    byte[] bytes = new byte[1024];
                    BinaryWriter binaryWriter = new BinaryWriter(fs);
                    int count = user.dataSession.binaryReader.Read(bytes, 0, bytes.Length);
                    while (count > 0)
                    {
                        binaryWriter.Write(bytes, 0, count);
                        binaryWriter.Flush();
                        count = user.dataSession.binaryReader.Read(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    StreamWriter streamWriter = new StreamWriter(fs);
                    while (user.dataSession.streamReader.Peek() > -1)
                    {
                        streamWriter.Write(user.dataSession.streamReader.ReadLine());
                        streamWriter.Flush();
                    }
                }
                Console.WriteLine("...]接收完毕");
            }
            finally
            {
                user.dataSession.Close();
                fs.Close();
            }
        }

        public static System.Numerics.BigInteger getDirSize(DirectoryInfo dir)
        {
            if (dir == null || !dir.Exists)
            {
                return 0;
            }
            else
            {
                Stack<DirectoryInfo> dir_stack = new Stack<DirectoryInfo>();
                dir_stack.Push(dir);
                System.Numerics.BigInteger dirsize = 0;
                while (dir_stack.Count>0)
                {
                    DirectoryInfo tmp = dir_stack.Pop();
                    DirectoryInfo[] dirs = tmp.GetDirectories();
                    FileInfo[] files = tmp.GetFiles();
                    foreach (FileInfo f in files)
                    {
                        dirsize += f.Length;
                    }
                    foreach (DirectoryInfo d in dirs)
                    {
                        dir_stack.Push(d);
                    }
                    
                }
                return dirsize;
            }
        }
    }
}
