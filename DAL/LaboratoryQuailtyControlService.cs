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
        /// <param name="qualityControl"></param>
        /// <returns></returns>
        public int InsertLaboratoryQuailtyControl(LaboratoryQuailtyControl qualityControl)
        {
            string sql = "INSERT INTO LaboratoryQuailtyControl(LaboratoryQuailtyControlId, ProductName, Description, Img, CategoryId, CategoryName, Analyte, Stability, Feature, Annotation, CreateTime, ModifyTIme, State) VALUES";
            sql += "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12});";
            sql = string.Format(sql,
                qualityControl.LaboratoryQualityControlId,
                qualityControl.ProductName,
                qualityControl.Description,
                qualityControl.Img,
                qualityControl.CategoryId,
                qualityControl.CategoryName,
                qualityControl.Analyte,
                qualityControl.Stability,
                qualityControl.Feature,
                qualityControl.Annotation,
                qualityControl.CreateTime,
                qualityControl.ModifyTime,
                qualityControl.State);

            string sqlSpec = "";

            foreach (LaboratorySpecification spec in qualityControl.Specifications)
            {
                if(sqlSpec == "")
                {
                    sqlSpec = "INSERT INTO LaboratorySpecification(SpecificationId, LaboratoryQualityControlId, ProductCode, Concentration, Specification, CertificateNo) VALUES";
                }

                string tmpSql = "('{0}','{1}','{2}','{3}','{4}','{5}'),";
                tmpSql = string.Format(tmpSql,
                    spec.SpecificationId,
                    spec.LaboratoryQualityControlId,
                    spec.ProductCode,
                    spec.Concentration,
                    spec.Specification,
                    spec.CertificateNo);
                sqlSpec += tmpSql;
            }

            if (sqlSpec.Length > 0)
            {
                sqlSpec = sqlSpec.Substring(0, sqlSpec.Length - 1);
                sqlSpec += ";";
            }

            sql += sqlSpec;

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
        /// <param name="qualityControl"></param>
        /// <returns></returns>
        public int UpdateLaboratoryQuailtyControl(LaboratoryQuailtyControl qualityControl)
        {
            List<LaboratorySpecification> labSpecs = qualityControl.Specifications;

            string delSql = "DELETE FROM LaboratorySpecification WHERE SpecificationId in (SELECT SpecificationId FROM LaboratorySpecification WHERE LaboratoryQualityControlId = '{0}');";
            delSql = string.Format(delSql, qualityControl.LaboratoryQualityControlId);

            SQLHelper.Update(delSql);

            if (labSpecs.Count > 0)
            {
                string sqlSpec = "";

                foreach (LaboratorySpecification spec in qualityControl.Specifications)
                {
                    if (sqlSpec == "")
                    {
                        sqlSpec = "INSERT INTO LaboratorySpecification(SpecificationId, LaboratoryQualityControlId, ProductCode, Concentration, Specification, CertificateNo) VALUES";
                    }

                    string tmpSql = "('{0}','{1}','{2}','{3}','{4}','{5}'),";
                    tmpSql = string.Format(tmpSql,
                        spec.SpecificationId,
                        spec.LaboratoryQualityControlId,
                        spec.ProductCode,
                        spec.Concentration,
                        spec.Specification,
                        spec.CertificateNo);
                    sqlSpec += tmpSql;
                }

                if (sqlSpec.Length > 0)
                {
                    sqlSpec = sqlSpec.Substring(0, sqlSpec.Length - 1);
                    sqlSpec += ";";
                }

                SQLHelper.Update(sqlSpec);
                
            }

            string updateSql = "UPDATE LaboratoryQuailtyControl SET ProductName = '{1}', Description = '{2}', Img = '{3}', CategoryId = '{4}', CategoryName = '{5}', Analyte = '{6}', Stability = '{7}', Feature = '{8}', Annotation = '{9}', ModifyTime = '{10}' WHERE LaboratoryQuailtyControlId = '{0}';";
            updateSql = string.Format(updateSql,
                qualityControl.LaboratoryQualityControlId,
                qualityControl.ProductName,
                qualityControl.Description,
                qualityControl.Img,
                qualityControl.CategoryId,
                qualityControl.CategoryName,
                qualityControl.Analyte,
                qualityControl.Stability,
                qualityControl.Feature,
                qualityControl.Annotation,
                qualityControl.ModifyTime);

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
            string sql = "SELECT State from LaboratoryQuailtyControl";
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

            sql = "UPDATE LaboratoryQuailtyControl set State = {0} where LaboratoryQuailtyControlId = '{1}'";
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
                labQualityControl.Analyte = reader["Analyte"].ToString();
                labQualityControl.Stability = reader["Stability"].ToString();
                labQualityControl.Feature = reader["Feature"].ToString();
                labQualityControl.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                labQualityControl.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                labQualityControl.Annotation = reader["Annotation"].ToString();
                labQualityControl.State = Convert.ToInt16(reader["State"]);
            }

            labQualityControl.Specifications = new List<LaboratorySpecification>();

            string specSql = "SELECT * LaboratorySpecification WHERE LaboratoryQualityControlId = '{0}'";
            specSql = string.Format(specSql, labQualityControl.LaboratoryQualityControlId);

            reader = SQLHelper.GetReader(specSql);

            while (reader.Read())
            {
                labQualityControl.Specifications.Add(new LaboratorySpecification()
                {
                    SpecificationId = reader["SpecificationId"].ToString(),
                    LaboratoryQualityControlId = reader["LaboratoryQualityControlId"].ToString(),
                    ProductCode = reader["ProductCode"].ToString(),
                    Concentration = reader["Concentration"].ToString(),
                    Specification = reader["Specification"].ToString(),
                    CertificateNo = reader["CertificateNo"].ToString(),
                });
            }

            return labQualityControl;
        }

        /// <summary>
        /// 根据id精确查询质控品名称
        /// </summary>
        /// <param name="id">质控品ID</param>
        /// <returns></returns>
        public LaboratoryQuailtyControl GetLaboratoryQuailtyControlDetail(string id)
        {
            LaboratoryQuailtyControl qualityControl = null;

            string sql = "SELECT * FROM LaboratoryQuailtyControl WHERE LaboratoryQuailtyControlId = '{0}'";
            sql = string.Format(sql, id);

            SqlDataReader reader = SQLHelper.GetReader(sql);

            if (reader.Read())
            {
                qualityControl = new LaboratoryQuailtyControl();

                qualityControl.LaboratoryQualityControlId = (reader["LaboratoryQuailtyControlId"] == null) ? "" : reader["LaboratoryQuailtyControlId"].ToString();
                qualityControl.ProductName = (reader["ProductName"] == null) ? "" : reader["ProductName"].ToString();
                qualityControl.Description = (reader["Description"] == null) ? "" : reader["Description"].ToString();
                qualityControl.Img = (reader["Img"] == null) ? "" : reader["Img"].ToString();
                qualityControl.CategoryId = (reader["CategoryId"] == null) ? "" : reader["CategoryId"].ToString();
                qualityControl.CategoryName = (reader["CategoryName"] == null) ? "" : reader["CategoryName"].ToString();
                qualityControl.Analyte = (reader["Analyte"] == null) ? "" : reader["Analyte"].ToString();
                qualityControl.Stability = (reader["Stability"] == null) ? "" : reader["Stability"].ToString();
                qualityControl.Feature = (reader["Feature"] == null) ? "" : reader["Feature"].ToString();
                qualityControl.Annotation = (reader["Annotation"] == null) ? "" : reader["Annotation"].ToString();
                qualityControl.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                qualityControl.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                qualityControl.State = (reader["State"] == null) ? 0 : Convert.ToInt16(reader["State"]);
            }

            if (qualityControl.LaboratoryQualityControlId != null && qualityControl.LaboratoryQualityControlId != "")
            {
                qualityControl.Specifications = new List<LaboratorySpecification>();

                string sqlSpec = "SELECT * FROM LaboratorySpecification WHERE LaboratoryQualityControlId = '{0}' ORDER BY ProductCode";
                sqlSpec = string.Format(sqlSpec, qualityControl.LaboratoryQualityControlId);

                SqlDataReader  specReader = SQLHelper.GetReader(sqlSpec);

                while (specReader.Read())
                {
                    qualityControl.Specifications.Add(new LaboratorySpecification()
                    {
                        SpecificationId = (specReader["SpecificationId"] == null) ? "" : specReader["SpecificationId"].ToString(),
                        LaboratoryQualityControlId = (specReader["LaboratoryQualityControlId"] == null) ? "" : specReader["LaboratoryQualityControlId"].ToString(),
                        ProductCode = (specReader["ProductCode"] == null) ? "" : specReader["ProductCode"].ToString(),
                        Concentration = (specReader["Concentration"] == null) ? "" : specReader["Concentration"].ToString(),
                        Specification = (specReader["Specification"] == null) ? "" : specReader["Specification"].ToString(),
                        CertificateNo = (specReader["CertificateNo"] == null) ? "" : specReader["CertificateNo"].ToString()
                    });
                }
            }

            return qualityControl;
        }

        /// <summary>
        /// 根据质控品名称模糊查询质控品集合
        /// </summary>
        /// <param name="qualityControlName">名称</param>
        /// <returns></returns>
        public List<LaboratoryQuailtyControl> GetLaboratoryQuailtyControls(string qualityControlName)
        {
            List<LaboratoryQuailtyControl> qualityControls = new List<LaboratoryQuailtyControl>();

            string sql = "SELECT * FROM LaboratoryQuailtyControl WHERE ProductName LIKE '%{0}%' ORDER BY CreateTime ASC";
            sql = string.Format(sql, qualityControlName);
            
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
                    State = Convert.ToInt16(reader["State"])
                });
            }

            return qualityControls;
        }

        /// <summary>
        /// 根据专业名称模糊查询质控品集合
        /// </summary>
        /// <param name="categoryName">专业名称</param>
        /// <returns></returns>
        public List<LaboratoryQuailtyControl> GetLaboratoryQuailtyControlsByCategory(string categoryName)
        {
            List<LaboratoryQuailtyControl> qualityControls = new List<LaboratoryQuailtyControl>();

            string sql = "SELECT * FROM LaboratoryQuailtyControl WHERE CategoryName = '{0}' AND State = 1 ORDER BY CreateTime ASC";
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
                    State = Convert.ToInt16(reader["State"])
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
                sql += " WHERE State = 1";
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
                    State = Convert.ToInt16(reader["State"])
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
