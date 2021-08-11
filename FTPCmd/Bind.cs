using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.FTPCmd
{
    class Bind:Attribute
    {
        /// <summary>
        /// 该特性用于解决命令重复实现的问题，目的为了满足多个不同名称的命令能有同一个实现，提高代码复用性。
        /// 用法：将目标函数的Type传入classname，即可实现将源命令的名称绑定到目标命令的实现。
        /// 即Command.commands[源命令]=目标命令
        /// </summary>
        public Type classname { get; set; }
    }
}