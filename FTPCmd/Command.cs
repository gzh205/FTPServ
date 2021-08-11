using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// FTP命令（用于提供客户端与服务器之间的FTP功能，
/// 是FTP协议中规定的命令，而非该软件自定义的命令）
/// </summary>
namespace FTPServ.FTPCmd
{
    public abstract class Command
    {
        /// <summary>
        /// 加载命令(就是FTPServ.FTPCmd.impl里的几个类)
        /// 注:利用反射实现自动加载FTPServ.FTPCmd.impl命名空间下的所有命令
        /// 不能在FTPServ.FTPCmd.impl下添加无关的类或者不是Command的子类
        /// </summary>
        public static void init()
        {
            Type[] types = Assembly.GetEntryAssembly().GetTypes();
            foreach (Type t in types)
            {
                if (t.FullName.Contains("FTPServ.FTPCmd.impl"))
                {
                    Attribute attr = t.GetCustomAttribute(typeof(Bind));
                    if (attr == null)
                        commands.Add(t.Name, Activator.CreateInstance(t) as Command);
                    else {
                        Bind bind = attr as Bind;
                        commands.Add(t.Name, Activator.CreateInstance(bind.classname) as Command);
                    }
                }
            }
        }
        private static Dictionary<string, Command> commands = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 根据命令字符串调用处理该命令的函数，若命令不存在，则抛出异常
        /// </summary>
        /// <param name="cmd">命令字符串</param>
        /// <param name="param">参数列表</param>
        public static void call(string cmd,FTPCore.User user,string param)
        {
            Command c = null;
            try
            {
                c = commands[cmd];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                Console.WriteLine("命名空间FTPCmd.impl下没有定义命令:"+ cmd);
                c = commands["NotImpl"];
            }
            c.user = user;
            c.param = param;
            c.operation();
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public FTPCore.User user { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string param { get; set; }
        /// <summary>
        /// FTP命令的实现
        /// </summary>
        public abstract void operation();
    }
}
