using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 软件中自定义的命令
/// </summary>
namespace FTPServ.SysCommand
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
                if (t.FullName.Contains("FTPServ.SysCommand.impl"))
                {
                    commands.Add(t.Name, Activator.CreateInstance(t) as Command);
                }
            }
        }
        protected static Dictionary<string, Command> commands = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 根据命令字符串调用处理该命令的函数，若命令不存在，则抛出异常
        /// </summary>
        /// <param name="cmd">命令字符串</param>
        /// <param name="param">参数列表</param>
        public static void call(string cmd, string param)
        {
            Command c = null;
            try
            {
                c = commands[cmd];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                Console.WriteLine("命名空间SysCommand.impl下没有定义命令:" + cmd);
                return;
            }
            c.param = param;
            c.operation();
        }
        /// <summary>
        /// 命令的描述信息
        /// </summary>
        public abstract string discription { get; }
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
