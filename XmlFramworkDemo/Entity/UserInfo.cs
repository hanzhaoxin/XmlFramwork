/* 
 类：UserInfo
 描述：XML实体类UserInfo
 编 码 人：韩兆新 日期：2014年12月21日

 修改记录：

*/
using XmlFramwork;

namespace XmlFramworkDemo.Entity
{
    public class UserInfo:XmlEntity
    {
        public string Name { set; get; }
        public uint Age { set; get; }
    }
}
