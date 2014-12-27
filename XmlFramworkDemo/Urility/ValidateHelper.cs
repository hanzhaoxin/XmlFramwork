/* 
 类：ValidateHelper
 描述：用于字符串格式验证
 编 码 人：韩兆新 日期：2014年12月21日

 修改记录：

*/
using System;

namespace XmlFramworkDemo.Urility
{
    public static class ValidateHelper
    {
        public static bool IsValidUintFormat(string strIn)
        {
            uint temp;
            return UInt32.TryParse(strIn,out temp);
        }
    }
}
