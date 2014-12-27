/* 
 类：PathRoute
 描述：路由XML数据文件路径
 编 码 人：韩兆新 日期：2014年12月21日

 修改记录：

*/
using System.Configuration;
using System.IO;
using System.Reflection;

namespace XmlFramwork
{
    static class PathRoute
    {
        public static readonly string DataFolder = ConfigurationManager.AppSettings["DataFolder"];
        public static string GetXmlPath<T>()
        {
            string dataFolder = DataFolder;
            if (string.IsNullOrEmpty(dataFolder))
            {
                dataFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
            }
            return Path.ChangeExtension(Path.Combine(dataFolder, Path.Combine(typeof(T).FullName.Split('.'))), ".xml");
        }
    }
}