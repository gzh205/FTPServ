using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPServ.Config
{
    public class ConfigManager
    {
        public static Dictionary<string, string> config_string = new Dictionary<string, string>();
        public static void ReadConfig()
        {
            try
            {
                StreamReader sr = new StreamReader(new FileStream("config.txt", FileMode.Open));
                string[] cfgdat = sr.ReadToEnd().Split('\n');
                foreach(string c in cfgdat)
                {
                    string tmp = c.Substring(0, c.IndexOf('#') - 1);
                    string[] splited_tmp = tmp.Split('=');
                    if (splited_tmp.Length == 0)
                    {
                        continue;
                    }
                    if (splited_tmp.Length != 2)
                    {
                        Console.WriteLine("配置文件有无法解析的命令");
                        throw new Exceptions.ConfigRepetited();
                    }
                    config_string.Add(splited_tmp[0].Trim(' ', '\t', '\r'), splited_tmp[1].Trim(' ', '\t', '\r'));
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("找不到配置文件config.txt");
            }
            catch (Exceptions.ConfigRepetited)
            {
                Console.WriteLine("配置文件中有错误的项");
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("配置文件中有重复的项");
            }
        }
    }
}
