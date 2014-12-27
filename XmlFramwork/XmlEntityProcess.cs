/* 
 类：XmlEntityProcess〈T〉
 描述：对Xml文档进行增、删、改、查等处理
 编 码 人：韩兆新 日期：2014年12月21日

 修改记录：

*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace XmlFramwork
{
    public static class XmlEntityProcess<T> where T : XmlEntity
    {
        private static string lastErrMsg;
        /// <summary>
        /// 获取最后一次错误的信息
        /// </summary>
        /// <returns></returns>
        public static string GetLastErrMsg()
        {
            return lastErrMsg;
        }
        /// <summary>
        /// 插入XML实体类对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool Insert(T entity)
        {
            try
            {
                List<T> entityList = new List<T>().Load();
                entity.ID = Guid.NewGuid();
                entityList.Add(entity);
                entityList.Save();
                return true;
            }
            catch (Exception ex)
            {
                lastErrMsg = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 根据ID删除XML实体类对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteById(Guid id)
        {
            try
            {
                List<T> entityList = new List<T>().Load();
                entityList = entityList.Where(entity => entity.ID != id).ToList();
                entityList.Save();
                return true;
            }
            catch (Exception ex)
            {
                lastErrMsg = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 更新XML实体类对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool Update(T entity)
        {
            try
            {
                List<T> entityList = new List<T>().Load();
                entityList = entityList.Where(e => e.ID != entity.ID).ToList();
                entityList.Add(entity);
                entityList.Save();

                return true;
            }
            catch (Exception ex)
            {
                lastErrMsg = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 获取所有的指定类型的XML实体类对象
        /// </summary>
        /// <returns></returns>
        public static List<T> GetAll()
        {
            try
            {
                List<T> entityList = new List<T>().Load();
                return entityList;
            }
            catch (Exception ex)
            {
                lastErrMsg = ex.Message;
                return null;
            }

        }
        /// <summary>
        /// 根据ID获取指定类型的XML实体类对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetById(Guid id)
        {
            try
            {
                List<T> entityList = new List<T>().Load();
                entityList = entityList.Where(e => e.ID == id).ToList();
                if (null == entityList || entityList.Count <= 0)
                {
                    return default(T);
                }
                else
                {
                    return entityList[0];
                }
            }
            catch (Exception ex)
            {
                lastErrMsg = ex.Message;
                return null;
            }
        }
    }
}
