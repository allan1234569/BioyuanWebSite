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
    /// 室内质控品查询类
    /// </summary>
    public class LaboratoryQuailtyControlService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="labQualityControl"></param>
        /// <returns></returns>
        public int InsertLaboratoryQuailtyControl(LaboratoryQuailtyControl labQualityControl)
        {
            string sql = "INSERT INTO LaboratoryQuailtyControl(LaboratoryQuailtyControlId, ProductName, Description, Img, CategoryId, CategoryName, Concentration, SingleSpecification, PackingSpecification, Status, StorageCondition, UsefulLife, PreservationStability, ProductMatrix, ContainedItems, CreateTime, ModifyTIme, Enable) VALUES";
            sql += "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}',{16});";
            sql = string.Format(sql,
                labQualityControl.LaboratoryQualityControlId,//0
                labQualityControl.ProductName,//1
                labQualityControl.Description,//2
                labQualityControl.Img,//3
                labQualityControl.CategoryId,//4
                labQualityControl.CategoryName,//5
                labQualityControl.Concentration,//6
                labQualityControl.SingleSpecification,//7
                labQualityControl.PackingSpecification,//8
                labQualityControl.Status,//9
                labQualityControl.StorageCondition,//10
                labQualityControl.UsefulLife,//11
                labQualityControl.PreservationStability,//12
                labQualityControl.ProductMatrix,//13
                labQualityControl.ContainedItems,//14
                labQualityControl.CreateTime,//15
                labQualityControl.ModifyTime,//16
                labQualityControl.Enable);//17

            //string sqlSpec = "";

            //foreach (LaboratorySpecification spec in qualityControl.Specifications)
            //{
            //    if(sqlSpec == "")
            //    {
            //        sqlSpec = "INSERT INTO LaboratorySpecification(SpecificationId, LaboratoryQualityControlId, ProductCode, Concentration, Specification, CertificateNo) VALUES";
            //    }

            //    string tmpSql = "('{0}','{1}','{2}','{3}','{4}','{5}'),";
            //    tmpSql = string.Format(tmpSql,
            //        spec.SpecificationId,
            //        spec.LaboratoryQualityControlId,
            //        spec.ProductCode,
            //        spec.Concentration,
            //        spec.Specification,
            //        spec.CertificateNo);
            //    sqlSpec += tmpSql;
            //}

            //if (sqlSpec.Length > 0)
            //{
            //    sqlSpec = sqlSpec.Substring(0, sqlSpec.Length - 1);
            //    sqlSpec += ";";
            //}

            //sql += sqlSpec;

            return SQLHelper.Update(sql);
        }

        /// <summary>
        /// 删除室内质控品
        /// </summary>
        /// <param name="qualityControlId"></param>
        /// <returns></returns>
        public int DeleteLaboratoryQuailtyControl(string qualityControlId)
        {
            string sql = "DELETE FROM LaboratorySpecification WHERE SpecificationId in (SELECT SpecificationId FROM LaboratorySpecification WHERE LaboratoryQualityControlId = '{0}');";
            sql += "DELETE FROM LaboratoryQuailtyControl WHERE LaboratoryQuailtyControlId = '{0}';";
            sql = string.Format(sql, qualityControlId);

            return SQLHelper.Update(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="labQualityControl"></param>
        /// <returns></returns>
        public int UpdateLaboratoryQuailtyControl(LaboratoryQuailtyControl labQualityControl)
        {
            //List<LaboratorySpecification> labSpecs = qualityControl.Specifications;

            //string delSql = "DELETE FROM LaboratorySpecification WHERE SpecificationId in (SELECT SpecificationId FROM LaboratorySpecification WHERE LaboratoryQualityControlId = '{0}');";
            //delSql = string.Format(delSql, qualityControl.LaboratoryQualityControlId);

            //SQLHelper.Update(delSql);

            //if (labSpecs.Count > 0)
            //{
            //    string sqlSpec = "";

            //    foreach (LaboratorySpecification spec in qualityControl.Specifications)
            //    {
            //        if (sqlSpec == "")
            //        {
            //            sqlSpec = "INSERT INTO LaboratorySpecification(SpecificationId, LaboratoryQualityControlId, ProductCode, Concentration, Specification, CertificateNo) VALUES";
            //        }

            //        string tmpSql = "('{0}','{1}','{2}','{3}','{4}','{5}'),";
            //        tmpSql = string.Format(tmpSql,
            //            spec.SpecificationId,
            //            spec.LaboratoryQualityControlId,
            //            spec.ProductCode,
            //            spec.Concentration,
            //            spec.Specification,
            //            spec.CertificateNo);
            //        sqlSpec += tmpSql;
            //    }

            //    if (sqlSpec.Length > 0)
            //    {
            //        sqlSpec = sqlSpec.Substring(0, sqlSpec.Length - 1);
            //        sqlSpec += ";";
            //    }

            //    SQLHelper.Update(sqlSpec);
                
            //}

            string updateSql = "UPDATE LaboratoryQuailtyControl SET ProductName = '{1}', Description = '{2}', Img = '{3}', CategoryId = '{4}', CategoryName = '{5}', Concentration = '{6}', SingleSpecification = '{7}', PackingSpecification = '{8}', Status = '{9}', StorageCondition = '{10}', UsefulLife = '{11}', PreservationStability = '{12}', ProductMatrix = '{13}', ContainedItems = '{14}', ModifyTime = '{15}' WHERE LaboratoryQuailtyControlId = '{0}';";
            updateSql = string.Format(updateSql,
                labQualityControl.LaboratoryQualityControlId,//0
                labQualityControl.ProductName,//1
                labQualityControl.Description,//2
                labQualityControl.Img,//3
                labQualityControl.CategoryId,//4
                labQualityControl.CategoryName,//5
                labQualityControl.Concentration,//6
                labQualityControl.SingleSpecification,//7
                labQualityControl.PackingSpecification,//8
                labQualityControl.Status,//9
                labQualityControl.StorageCondition,//10
                labQualityControl.UsefulLife,//11
                labQualityControl.PreservationStability,//12
                labQualityControl.ProductMatrix,//13
                labQualityControl.ContainedItems,//14
                labQualityControl.ModifyTime);//15

            return SQLHelper.Update(updateSql);
        }


        /// <summary>
        /// 启用室内质控品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EnableLabratoryQualityControl(string id)
        {
            if (id == null || string.Empty == id)
            {
                return -1;
            }
            string sql = "SELECT Enable from LaboratoryQuailtyControl";
            sql += " where LaboratoryQuailtyControlId = '{0}'";
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

            sql = "UPDATE LaboratoryQuailtyControl set Enable = {0} where LaboratoryQuailtyControlId = '{1}'";
            sql = string.Format(sql, state, id);

            int retVal = SQLHelper.Update(sql);

            if (retVal > 0)
            {
                return state;
            }

            return -1;
        }

        public LaboratoryQuailtyControl GetLaboratoryQuailtyControlById(string qualityControlId)
        {
            LaboratoryQuailtyControl labQualityControl = new LaboratoryQuailtyControl();

            SqlDataReader reader = null;

            string sql = "SELECT * FROM LaboratoryQuailtyControl WHERE LaboratoryQuailtyControlId = '{0}'";
            sql = string.Format(sql, qualityControlId);

            reader = SQLHelper.GetReader(sql);

            if (reader.Read())
            {
                labQualityControl.LaboratoryQualityControlId = reader["LaboratoryQualityControlId"].ToString();
                labQualityControl.ProductName = reader["ProductName"].ToString();
                labQualityControl.Description = reader["Description"].ToString();
                labQualityControl.Img = reader["Img"].ToString();
                labQualityControl.CategoryId = (reader["CategoryId"] == null) ? "" : reader["CategoryId"].ToString();
                labQualityControl.CategoryName = (reader["CategoryName"] == null) ? "" : reader["CategoryName"].ToString();
                labQualityControl.Concentration = (reader["Concentration"] == null) ? "" : reader["Concentration"].ToString();
                labQualityControl.SingleSpecification = (reader["SingleSpecification"] == null) ? "" : reader["SingleSpecification"].ToString();
                labQualityControl.PackingSpecification = (reader["PackingSpecification"] == null) ? "" : reader["PackingSpecification"].ToString();
                labQualityControl.Status = (reader["Status"] == null) ? "" : reader["Status"].ToString();
                labQualityControl.StorageCondition = (reader["StorageCondition"] == null) ? "" : reader["StorageCondition"].ToString();
                labQualityControl.UsefulLife = (reader["UsefulLife"] == null) ? "" : reader["UsefulLife"].ToString();
                labQualityControl.PreservationStability = (reader["PreservationStability"] == null) ? "" : reader["PreservationStability"].ToString();
                labQualityControl.ProductMatrix = (reader["ProductMatrix"] == null) ? "" : reader["ProductMatrix"].ToString();
                labQualityControl.ContainedItems = (reader["ContainedItems"] == null) ? "" : reader["ContainedItems"].ToString();
                labQualityControl.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                labQualityControl.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                labQualityControl.Enable = (reader["Enable"] == null) ? 0 : Convert.ToInt16(reader["Enable"]);
            }

            //labQualityControl.Specifications = new List<LaboratorySpecification>();

            //string specSql = "SELECT * LaboratorySpecification WHERE LaboratoryQualityControlId = '{0}'";
            //specSql = string.Format(specSql, labQualityControl.LaboratoryQualityControlId);

            //reader = SQLHelper.GetReader(specSql);

            //while (reader.Read())
            //{
            //    labQualityControl.Specifications.Add(new LaboratorySpecification()
            //    {
            //        SpecificationId = reader["SpecificationId"].ToString(),
            //        LaboratoryQualityControlId = reader["LaboratoryQualityControlId"].ToString(),
            //        ProductCode = reader["ProductCode"].ToString(),
            //        Concentration = reader["Concentration"].ToString(),
            //        Specification = reader["Specification"].ToString(),
            //        CertificateNo = reader["CertificateNo"].ToString(),
            //    });
            //}

            return labQualityControl;
        }

        /// <summary>
        /// 根据id精确查询质控品名称
        /// </summary>
        /// <param name="id">质控品ID</param>
        /// <returns></returns>
        public LaboratoryQuailtyControl GetLaboratoryQuailtyControlDetail(string id)
        {
            LaboratoryQuailtyControl labQualityControl = null;

            string sql = "SELECT * FROM LaboratoryQuailtyControl WHERE LaboratoryQuailtyControlId = '{0}'";
            sql = string.Format(sql, id);

            SqlDataReader reader = SQLHelper.GetReader(sql);

            if (reader.Read())
            {
                labQualityControl = new LaboratoryQuailtyControl();

                labQualityControl.LaboratoryQualityControlId = reader["LaboratoryQuailtyControlId"].ToString();
                labQualityControl.ProductName = reader["ProductName"].ToString();
                labQualityControl.Description = reader["Description"].ToString();
                labQualityControl.Img = reader["Img"].ToString();
                labQualityControl.CategoryId = (reader["CategoryId"] == null) ? "" : reader["CategoryId"].ToString();
                labQualityControl.CategoryName = (reader["CategoryName"] == null) ? "" : reader["CategoryName"].ToString();
                labQualityControl.Concentration = (reader["Concentration"] == null) ? "" : reader["Concentration"].ToString();
                labQualityControl.SingleSpecification = (reader["SingleSpecification"] == null) ? "" : reader["SingleSpecification"].ToString();
                labQualityControl.PackingSpecification = (reader["PackingSpecification"] == null) ? "" : reader["PackingSpecification"].ToString();
                labQualityControl.Status = (reader["Status"] == null) ? "" : reader["Status"].ToString();
                labQualityControl.StorageCondition = (reader["StorageCondition"] == null) ? "" : reader["StorageCondition"].ToString();
                labQualityControl.UsefulLife = (reader["UsefulLife"] == null) ? "" : reader["UsefulLife"].ToString();
                labQualityControl.PreservationStability = (reader["PreservationStability"] == null) ? "" : reader["PreservationStability"].ToString();
                labQualityControl.ProductMatrix = (reader["ProductMatrix"] == null) ? "" : reader["ProductMatrix"].ToString();
                labQualityControl.ContainedItems = (reader["ContainedItems"] == null) ? "" : reader["ContainedItems"].ToString();
                labQualityControl.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                labQualityControl.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                labQualityControl.Enable = (reader["Enable"] == null) ? 0 : Convert.ToInt16(reader["Enable"]);
            }

            //if (labQualityControl.LaboratoryQualityControlId != null && labQualityControl.LaboratoryQualityControlId != "")
            //{
            //    labQualityControl.Specifications = new List<LaboratorySpecification>();

            //    string sqlSpec = "SELECT * FROM LaboratorySpecification WHERE LaboratoryQualityControlId = '{0}' ORDER BY ProductCode";
            //    sqlSpec = string.Format(sqlSpec, labQualityControl.LaboratoryQualityControlId);

            //    SqlDataReader  specReader = SQLHelper.GetReader(sqlSpec);

            //    while (specReader.Read())
            //    {
            //        labQualityControl.Specifications.Add(new LaboratorySpecification()
            //        {
            //            SpecificationId = (specReader["SpecificationId"] == null) ? "" : specReader["SpecificationId"].ToString(),
            //            LaboratoryQualityControlId = (specReader["LaboratoryQualityControlId"] == null) ? "" : specReader["LaboratoryQualityControlId"].ToString(),
            //            ProductCode = (specReader["ProductCode"] == null) ? "" : specReader["ProductCode"].ToString(),
            //            Concentration = (specReader["Concentration"] == null) ? "" : specReader["Concentration"].ToString(),
            //            Specification = (specReader["Specification"] == null) ? "" : specReader["Specification"].ToString(),
            //            CertificateNo = (specReader["CertificateNo"] == null) ? "" : specReader["CertificateNo"].ToString()
            //        });
            //    }
            //}

            return labQualityControl;
        }

        /// <summary>
        /// 根据质控品名称模糊查询质控品集合
        /// </summary>
        /// <param name="qualityControlName">名称</param>
        /// <returns></returns>
        public List<LaboratoryQuailtyControl> GetLaboratoryQuailtyControls(string qualityControlName)
        {
            List<LaboratoryQuailtyControl> labQualityControl = new List<LaboratoryQuailtyControl>();

            string sql = "SELECT * FROM LaboratoryQuailtyControl WHERE ProductName LIKE '%{0}%' ORDER BY CreateTime ASC";
            sql = string.Format(sql, qualityControlName);
            
            SqlDataReader reader = SQLHelper.GetReader(sql);


            while (reader.Read())
            {
                labQualityControl.Add(new LaboratoryQuailtyControl()
                {
                    LaboratoryQualityControlId = reader["LaboratoryQuailtyControlId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Img = reader["Img"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                    Enable = Convert.ToInt16(reader["Enable"])
                });
            }

            return labQualityControl;
        }

        /// <summary>
        /// 根据专业名称模糊查询质控品集合
        /// </summary>
        /// <param name="categoryName">专业名称</param>
        /// <returns></returns>
        public List<LaboratoryQuailtyControl> GetLaboratoryQuailtyControlsByCategory(string categoryName)
        {
            List<LaboratoryQuailtyControl> qualityControls = new List<LaboratoryQuailtyControl>();

            string sql = "SELECT * FROM LaboratoryQuailtyControl WHERE CategoryName = '{0}' AND Enable = 1 ORDER BY CreateTime ASC";
            sql = string.Format(sql, categoryName);

            SqlDataReader reader = SQLHelper.GetReader(sql);


            while (reader.Read())
            {
                qualityControls.Add(new LaboratoryQuailtyControl()
                {
                    LaboratoryQualityControlId = reader["LaboratoryQuailtyControlId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Img = reader["Img"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                    Enable = Convert.ToInt16(reader["Enable"])
                });
            }

            return qualityControls;
        }

        /// <summary>
        /// 获取室内质控品集合
        /// </summary>
        /// <param name="state">是否启用状态信息过滤</param>
        /// <returns></returns>
        public List<LaboratoryQuailtyControl> GetLaboratoryQuailtyControls(bool state = false)
        {
            string sql = "SELECT * FROM LaboratoryQuailtyControl";
            
            if(state){
                sql += " WHERE Enable = 1";
            }

            sql += " ORDER BY CreateTime ASC";

            List<LaboratoryQuailtyControl> qualityControls = new List<LaboratoryQuailtyControl>();

            SqlDataReader reader = SQLHelper.GetReader(sql);

            while (reader.Read())
            {
                qualityControls.Add(new LaboratoryQuailtyControl()
                {
                    LaboratoryQualityControlId = reader["LaboratoryQuailtyControlId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Img = reader["Img"].ToString(),
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
            string sql = "SELECT Img FROM LaboratoryQuailtyControl WHERE LaboratoryQuailtyControlId = '{0}'";
            sql = string.Format(sql, id);

            return (string)SQLHelper.GetSingleResult(sql);
        }

        public List<string> GetCategorys()
        {
            List<string> list = new List<string>();

            string sql = "SELECT DISTINCT(CategoryName) FROM LaboratoryQuailtyControl ORDER BY CategoryName";

            SqlDataReader reader = SQLHelper.GetReader(sql);

            while (reader.Read())
            {
                list.Add(reader["CategoryName"].ToString());
            }

            return list;
        }
    }
}
