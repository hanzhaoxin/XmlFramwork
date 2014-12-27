/* 
 类：ListExtend
 描述：为List<T>扩展加载XML文档和保存为XML文档的方法
 编 码 人：韩兆新 日期：2014年12月21日

 修改记录：

*/
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlFramwork
{
    public static class ListExtend
    {
        /// <summary>
        /// 加载XML文档返回List集合
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<TSource> Load<TSource>(this List<TSource> source)
        {
            string fileName = PathRoute.GetXmlPath<TSource>();
            if (File.Exists(fileName))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TSource>));
                using (Stream reader = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    return xmlSerializer.Deserialize(reader) as List<TSource>;
                }
            }
            else
            {
                return new List<TSource>();
            }
        }
        /// <summary>
        /// 将list集合保存为XML文档
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        public static void Save<TSource>(this List<TSource> source)
        {
            string fileName = PathRoute.GetXmlPath<TSource>();
            FileInfo fileInfo = new FileInfo(fileName);
            DirectoryInfo directoryInfo = fileInfo.Directory;
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            using (Stream writer = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(writer, source);
            }
        }
    }
}
