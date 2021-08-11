using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SQLite.Generic;

namespace FTPServ.Config
{
    /// <summary>
    /// 为了能够让Sql正常调用，必须有无参构造函数，或者不显示定义构造函数
    /// </summary>
    public class User
    {
        public static Dictionary<string,User> users = new Dictionary<string, User>();
        public static Sql.Connection userConn;
        // 用户名
        [FTPServ.Sql.PrimaryKey]
        public string userName { get; set; }
        //密码
        public string password { get; set; }
        // 工作目录
        public string workDir { get; set; }
        public static void ReadUsers()
        {
            //创建新的SQLite数据库连接
            //FTPServ.Sql能够自动生成SQL语句
            userConn = new Sql.Connection("Data Source=" + ConfigManager.config_string["userdb_path"] + ";Version=3;New=True;Compress=True;", typeof(SQLiteConnection));
            //查询User表中的所有内容
            User[] u = userConn.SelectSome<User>("");
            //将查询结果添加进新的list中，方便其他模块读取
            foreach(User i in u)
            {
                users.Add(i.userName, i);
            }
        }
        public static void UpdateUser(User u)
        {
            if (users.ContainsKey(u.userName))
            {
                users[u.userName] = u;//更新缓存中的用户信息
                userConn.Update(u);//更新数据库中的用户信息
            }
            else
            {
                users.Add(u.userName, u);
                userConn.Insert(u);
            }
        }
        public static void InsertUser(User u)
        {
            users.Add(u.userName,u);
            userConn.Insert(u);
        }
        public static void DeleteUser(string username)
        {
            userConn.Delete(users[username]);
            users.Remove(username);
        }
    }
}
