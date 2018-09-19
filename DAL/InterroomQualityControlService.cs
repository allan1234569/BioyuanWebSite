using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL.Helper;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 室间质评查询类
    /// </summary>
    public class InterroomQualityControlService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityControl"></param>
        /// <returns></returns>
        public int InsertInterroomQualityControl(InterroomQualityControl qualityControl)
        {
            string sql = "INSERT INTO InterroomQualityControl(InterroomQualityControlId, ProductName, Description, Img, CategoryId, CategoryName, Concentration, SingleSpecification, PackingSpecification, Status, StorageCondition, UsefulLife, PreservationStability, ProductMatrix, ContainedItems, CreateTime, ModifyTime, Enable) VALUES";
            sql += "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}', '{15}','{16}',{17});";
            sql = string.Format(sql,
                qualityControl.InterroomQualityControlId, //0
                qualityControl.ProductName,//1
                qualityControl.Description,//2
                qualityControl.Img,//3
                qualityControl.CategoryId,//4
                qualityControl.CategoryName,//5
                qualityControl.Concentration,//6
                qualityControl.SingleSpecification,//7
                qualityControl.PackingSpecification,//8
                qualityControl.Status,//9
                qualityControl.StorageCondition,//10
                qualityControl.UsefulLife,//11
                qualityControl.PreservationStability,//12
                qualityControl.ProductMatrix,//13
                qualityControl.ContainedItems,//14
                qualityControl.CreateTime,//15
                qualityControl.ModifyTime,//16
                qualityControl.Enable);//17

            return SQLHelper.Update(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityControlId"></param>
        /// <returns></returns>
        public int DeleteInterroomQualityControl(string qualityControlId)
        {
            string sql = "DELETE FROM InterroomQualityControl WHERE InterroomQualityControlId = '{0}'";
            sql = string.Format(sql, qualityControlId);

            return SQLHelper.Update(sql);
        }


        /// <summary>
        /// 更新质控品信息
        /// </summary>
        /// <param name="qualityControl"></param>
        /// <returns></returns>
        public int UpdateInterroomQualityControl(InterroomQualityControl qualityControl)
        {
            string sql = "UPDATE InterroomQualityControl SET ProductName = '{1}', Description = '{2}', Img = '{3}', CategoryId = '{4}', CategoryName = '{5}', Concentration = '{6}', SingleSpecification = '{7}', PackingSpecification = '{8}', Status = '{9}', StorageCondition = '{10}', UsefulLife = '{11}', PreservationStability = '{12}', ProductMatrix = '{13}', ContainedItems = '{14}', ModifyTime = '{15}' WHERE InterroomQualityControlId = '{0}';";
            sql = string.Format(sql,
                qualityControl.InterroomQualityControlId,//0
                qualityControl.ProductName,//1
                qualityControl.Description,//2
                qualityControl.Img,//3
                qualityControl.CategoryId,//4
                qualityControl.CategoryName,//5
                qualityControl.Concentration,//6
                qualityControl.SingleSpecification,//7
                qualityControl.PackingSpecification,//8
                qualityControl.Status,//9
                qualityControl.StorageCondition,//10
                qualityControl.UsefulLife,//11
                qualityControl.PreservationStability,//12
                qualityControl.ProductMatrix,//13
                qualityControl.ContainedItems,//14
                qualityControl.ModifyTime);//15

            return SQLHelper.Update(sql);
        }

        public int EnableInterroomQualityControl(string id)
        {
            if (id == null || string.Empty == id)
            {
                return -1;
            }
            string sql = "SELECT Enable from InterroomQualityControl";
            sql += " where InterroomQualityControlId = '{0}'";
            sql = string.Format(sql, id);

            int state = Convert.ToInt16(SQLHelper.GetSingleResult(sql));

            if (state == 0)
            {
                state = 1;
            }
            else
            {
                state = 0;
            }

            sql = "UPDATE InterroomQualityControl set Enable = {0} where InterroomQualityControlId = '{1}'";
            sql = string.Format(sql, state, id);

            int retVal = SQLHelper.Update(sql);

            if (retVal > 0)
            {
                return state;
            }

            return -1;
        }

        /// <summary>
        /// 根据id精确查询质控品名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InterroomQualityControl GetInterroomQualityControlDetail(string id)
        {
            InterroomQualityControl interroomQualityControl = null;

            string sql = "SELECT * FROM InterroomQualityControl WHERE InterroomQualityControlId = '{0}'";
            sql = string.Format(sql, id);
            SqlDataReader reader = SQLHelper.GetReader(sql);

            if (reader.Read())
            {
                interroomQualityControl = new InterroomQualityControl();

                interroomQualityControl.InterroomQualityControlId = reader["InterroomQualityControlId"].ToString();
                interroomQualityControl.ProductName = reader["ProductName"].ToString();
                interroomQualityControl.Description = reader["Description"].ToString();
                interroomQualityControl.Img = reader["Img"].ToString();
                interroomQualityControl.CategoryId = (reader["CategoryId"] == null) ? "" : reader["CategoryId"].ToString();
                interroomQualityControl.CategoryName = (reader["CategoryName"] == null) ? "" : reader["CategoryName"].ToString();
                interroomQualityControl.Concentration = reader["Concentration"].ToString();
                interroomQualityControl.SingleSpecification = reader["SingleSpecification"].ToString();
                interroomQualityControl.PackingSpecification = reader["PackingSpecification"].ToString();
                interroomQualityControl.Status = reader["Status"].ToString();
                interroomQualityControl.StorageCondition = reader["StorageCondition"].ToString();
                interroomQualityControl.UsefulLife = reader["UsefulLife"].ToString();
                interroomQualityControl.PreservationStability = reader["PreservationStability"].ToString();
                interroomQualityControl.ProductMatrix = reader["ProductMatrix"].ToString();
                interroomQualityControl.ContainedItems = reader["ContainedItems"].ToString();
                interroomQualityControl.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                interroomQualityControl.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                interroomQualityControl.Enable = Convert.ToInt16(reader["Enable"]);
            }

            return interroomQualityControl;
        }

        /// <summary>
        /// 根据名称模糊查询室间质控品集合
        /// </summary>
        /// <param name="qualityControlName"></param>
        /// <returns></returns>
        public List<InterroomQualityControl> GetInterroomQualityControls(string qualityControlName)
        {
            List<InterroomQualityControl> qualityControls = new List<InterroomQualityControl>();

            string sql = "SELECT * FROM InterroomQualityControl WHERE ProductName LIKE '%{0}%' ORDER BY CreateTime ASC";
            sql = string.Format(sql, qualityControlName);

            SqlDataReader reader = SQLHelper.GetReader(sql);

            while (reader.Read())
            {
                qualityControls.Add(new InterroomQualityControl()
                {
                    InterroomQualityControlId = reader["InterroomQualityControlId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Img = reader["Img"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                    Enable = Convert.ToInt16(reader["Enable"])
                });
            }

            return qualityControls;
        }

        /// <summary>
        /// 获取室间质控品列表
        /// </summary>
        /// <param name="state">是否是启用状态</param>
        /// <returns></returns>
        public List<InterroomQualityControl> GetInterroomQualityControls(bool state = false)
        {
            string sql = "SELECT * FROM InterroomQualityControl";
            if(state){
                sql += " WHERE Enable = 1";
                     
            }
            sql += " ORDER BY CreateTime ASC";

            List<InterroomQualityControl> qualityControls = new List<InterroomQualityControl>();

            SqlDataReader reader = SQLHelper.GetReader(sql);

            while (reader.Read())
            {
                qualityControls.Add(new InterroomQualityControl()
                {
                    InterroomQualityControlId = reader["InterroomQualityControlId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Description = reader["Description"].ToString(),
                    Img = reader["Img"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                    Enable = Convert.ToInt16(reader["Enable"])
                });
            }

            return qualityControls;
        }

        /// <summary>
        /// 根据专业名称获取室间质控品列表
        /// </summary>
        /// <param name="categoryName">专业名称</param>
        /// <returns></returns>
        public List<InterroomQualityControl> GetInterroomQualityControlsByCategory(string categoryName)
        {
            List<InterroomQualityControl> qualityControls = new List<InterroomQualityControl>();

            string sql = "SELECT * FROM InterroomQualityControl WHERE CategoryName = '{0}' AND Enable = 1 ORDER BY CreateTime ASC";
            sql = string.Format(sql, categoryName);

            SqlDataReader reader = SQLHelper.GetReader(sql);

            while (reader.Read())
            {
                qualityControls.Add(new InterroomQualityControl()
                {
                    InterroomQualityControlId = reader["InterroomQualityControlId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Img = reader["Img"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                    Enable = Convert.ToInt16(reader["Enable"])
                });
            }

            return qualityControls;
        }


        /// <summary>
        /// 根据id获取图片名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetImgPath(string id)
        {
            string sql = "SELECT Img FROM InterroomQualityControl WHERE InterroomQualityControlId = '{0}'";
            sql = string.Format(sql, id);

            return (string)SQLHelper.GetSingleResult(sql);
        }

        public List<string> GetCategorys()
        {
            List<string> list = new List<string>();

            string sql = "SELECT DISTINCT(CategoryName) FROM InterroomQualityControl ORDER BY CategoryName";

            SqlDataReader reader = SQLHelper.GetReader(sql);

            while (reader.Read())
            {
                list.Add(reader["CategoryName"].ToString());
            }

            return list;
        }
    }
}
