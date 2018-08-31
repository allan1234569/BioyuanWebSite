using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models;
using DAL.Helper;

namespace DAL
{
    /// <summary>
    /// 标准物质查询类
    /// </summary>
    public class MaterialService
    {
        /// <summary>
        /// 添加标准物质
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public int InsertMaterial(Material material)
        {
            string sql = "INSERT INTO Material(MaterialId, ProductName, Description, Img, CategoryId, CategoryName, RecommendedConcentration, Stability, Feature, Annotation, CreateTime, ModifyTime, State) Values";
            sql += "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}', '{9}', '{10}', '{11}', {12});";
            sql = string.Format(sql, 
                material.MaterialId, //0
                material.ProductName, //1
                material.Description, //2
                material.Img, //3
                material.CategoryId,//4
                material.CategoryName,//5
                material.RecommendedConcentration, //6
                material.Stability, //7
                material.Feature, //8
                material.Annotation,//9
                material.CreateTime,//10
                material.ModifyTime,//11
                material.State);//12

            string sqlpro = "";
            string sqlSpec = "";

            if(material.materialProjects.Count > 0){
                if (sqlpro == "")
                {
                    sqlpro += " INSERT INTO MaterialProject(MaterialProjectId, MaterialId, MaterialProjectName, Unit) Values";
                }
            
                foreach(MaterialProject pro in material.materialProjects)
                {
                    string tmpProSql = "('{0}', '{1}', '{2}', '{3}'),";
                    tmpProSql = string.Format(tmpProSql, pro.materialProjectId, pro.materialId, pro.materialProjectName, pro.unit);
                    sqlpro += tmpProSql;

                    if (pro.materialSpecifications.Count > 0)
                    {
                        if (sqlSpec == "")
                        {
                            sqlSpec += " INSERT INTO MaterialSpecification(SpecificationId, MaterialProjectId, ProductCode, StardardUncertairty, Specification, CertificateNo) Values";
                        }

                        foreach (MaterialSpecification spec in pro.materialSpecifications)
                        {
                            string tmpSpecSql = "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}'),";
                            tmpSpecSql = string.Format(tmpSpecSql, spec.SpecificationId, spec.MaterialProjectId, spec.ProductCode, spec.StardardUncertairty, spec.Specification, spec.CertificateNo);
                            sqlSpec += tmpSpecSql;
                        }   
                    }
                }

                if (sqlSpec.Length != 0)
                {
                    sqlSpec = sqlSpec.Substring(0, sqlSpec.Length - 1);
                    sqlSpec += ";";
                }
            }

            sqlpro = sqlpro.Substring(0, sqlpro.Length - 1);//去掉最后的逗号
            sqlpro += ";";//添加分号
            
            sql += sqlpro;
            sql += sqlSpec;

            return SQLHelper.Update(sql);
        }

        /// <summary>
        /// 根据ID删除标准物
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public int DeleteMaterialById(string materialId)
        {
            string sql = "DELETE FROM MaterialSpecification WHERE MaterialProjectId in (SELECT MaterialProjectId FROM MaterialProject WHERE MaterialId in (SELECT MaterialId FROM Material WHERE MaterialId = '{0}'));";
            sql += " DELETE FROM MaterialProject WHERE MaterialProjectId in (SELECT MaterialProjectId FROM MaterialProject WHERE MaterialId = '{0}');";
            sql += " DELETE FROM Material WHERE MaterialId = '{0}';";

            sql = string.Format(sql, materialId);

            return SQLHelper.Update(sql);
        }

        /// <summary>
        /// 更新标准物
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public int UpdateMaterialById(Material material)
        {
            if( DeleteMaterialById(material.MaterialId) > 0 )
            {
                return InsertMaterial(material);
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 启用标准物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EnableMaterial(string id)
        {
            if (id == null || string.Empty == id)
            {
                return -1;
            }
            string sql = "SELECT State from Material";
            sql += " where MaterialId = '{0}'";
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

            sql = "UPDATE Material set State = {0} where MaterialId = '{1}'";
            sql = string.Format(sql, state, id);

            int retVal = SQLHelper.Update(sql);

            if (retVal > 0)
            {
                return state;
            }

            return -1;
        }


        /// <summary>
        /// 根据id精确查询标准物详细信息
        /// </summary>
        /// <param name="id">标准物ID</param>
        /// <returns></returns>
        public Material GetMaterialDetail(string id)
        {
            string sql = "SELECT * FROM Material WHERE MaterialId = '{0}'";
            sql = string.Format(sql, id);

            SqlDataReader reader = SQLHelper.GetReader(sql);

            Material material = null;

            if (reader.Read())
            {
                material = new Material();

                material.MaterialId = reader["MaterialId"].ToString();
                material.ProductName = reader["ProductName"].ToString();
                material.Description = reader["Description"].ToString();
                material.Img = reader["Img"].ToString();
                material.CategoryId = (reader["CategoryId"] == null) ? "" : reader["CategoryId"].ToString();
                material.CategoryName = (reader["CategoryName"] == null) ? "" : reader["CategoryName"].ToString();
                material.RecommendedConcentration = reader["RecommendedConcentration"].ToString();
                material.Stability = reader["Stability"].ToString();
                material.Feature = reader["Feature"].ToString();
                material.Annotation = reader["Annotation"].ToString();
                material.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                material.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                material.State = Convert.ToInt16(reader["State"]);
            }

            if (material != null && material.MaterialId != "")
            {
                sql = "SELECT * FROM MaterialProject WHERE MaterialId = '{0}' ORDER BY MaterialProjectName";
                sql = string.Format(sql, material.MaterialId);
                reader = SQLHelper.GetReader(sql);

                material.materialProjects = new List<MaterialProject>();
                while (reader.Read())
                {
                    MaterialProject mp = new MaterialProject();
                    mp.materialProjectId = reader["materialProjectId"].ToString();
                    mp.materialId = reader["materialId"].ToString();
                    mp.materialProjectName = reader["materialProjectName"].ToString();
                    mp.unit = reader["Unit"].ToString();

                    if (mp.materialProjectId != "")
                    {
                        string specSql = "SELECT * FROM MaterialSpecification WHERE MaterialProjectId = '{0}' ORDER BY ProductCode";
                        specSql = string.Format(specSql, mp.materialProjectId);
                        SqlDataReader specReader = SQLHelper.GetReader(specSql);

                        mp.materialSpecifications = new List<MaterialSpecification>();

                        while (specReader.Read())
                        {
                            mp.materialSpecifications.Add(new MaterialSpecification()
                            {
                                SpecificationId = specReader["SpecificationId"].ToString(),
                                MaterialProjectId = specReader["MaterialProjectId"].ToString(),
                                ProductCode = specReader["ProductCode"].ToString(),
                                StardardUncertairty = specReader["StardardUncertairty"].ToString(),
                                Specification = specReader["Specification"].ToString(),
                                CertificateNo = specReader["CertificateNo"].ToString(),
                            });
                        }
                    }

                    material.materialProjects.Add(mp);
                }
            }

            return material;
        }


        public int UpdateMaterial(Material material)
        {
            string sql = "DELETE FROM MaterialSpecification WHERE MaterialProjectId in (SELECT MaterialProjectId FROM MaterialProject WHERE MaterialId in (SELECT MaterialId FROM Material WHERE MaterialId = '{0}'));";
            sql += " DELETE FROM MaterialProject WHERE MaterialProjectId in (SELECT MaterialProjectId FROM MaterialProject WHERE MaterialId = '{0}');";
            sql = string.Format(sql, material.MaterialId);

            SQLHelper.Update(sql);

            string sqlpro = "";
            string sqlSpec = "";

            if (material.materialProjects.Count > 0)
            {
                if (sqlpro == "")
                {
                    sqlpro += " INSERT INTO MaterialProject(MaterialProjectId, MaterialId, MaterialProjectName, Unit) Values";
                }

                foreach (MaterialProject pro in material.materialProjects)
                {
                    string tmpProSql = "('{0}', '{1}', '{2}', '{3}'),";
                    tmpProSql = string.Format(tmpProSql, pro.materialProjectId, pro.materialId, pro.materialProjectName, pro.unit);
                    sqlpro += tmpProSql;

                    if (pro.materialSpecifications.Count > 0)
                    {
                        if (sqlSpec == "")
                        {
                            sqlSpec += " INSERT INTO MaterialSpecification(SpecificationId, MaterialProjectId, ProductCode, StardardUncertairty, Specification, CertificateNo) Values";
                        }

                        foreach (MaterialSpecification spec in pro.materialSpecifications)
                        {
                            string tmpSpecSql = "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}'),";
                            tmpSpecSql = string.Format(tmpSpecSql, spec.SpecificationId, spec.MaterialProjectId, spec.ProductCode, spec.StardardUncertairty, spec.Specification, spec.CertificateNo);
                            sqlSpec += tmpSpecSql;
                        }
                    }
                }

                if (sqlSpec.Length != 0)
                {
                    sqlSpec = sqlSpec.Substring(0, sqlSpec.Length - 1);
                    sqlSpec += ";";
                }
            }

            sqlpro = sqlpro.Substring(0, sqlpro.Length - 1);//去掉最后的逗号
            sqlpro += ";";//添加分号

            sql = sqlpro + sqlSpec;

            SQLHelper.Update(sql);

            string updateSql = "UPDATE Material SET ProductName = '{1}', Description = '{2}', Img = '{3}', CategoryId = '{4}', CategoryName = '{5}', RecommendedConcentration = '{6}', Stability = '{7}', Feature = '{8}', Annotation = '{9}', ModifyTime = '{10}' WHERE MaterialId = '{0}'";
            updateSql = string.Format(updateSql,
                material.MaterialId,
                material.ProductName,
                material.Description,
                material.Img,
                material.CategoryId,
                material.CategoryName,
                material.RecommendedConcentration,
                material.Stability,
                material.Feature,
                material.Annotation,
                material.ModifyTime);

            return SQLHelper.Update(updateSql);
        }


        /// <summary>
        /// 根据标准物名称模糊查询标准物集合
        /// </summary>
        /// <param name="materialName">标准物名称</param>
        /// <returns></returns>
        public List<Material> GetMaterials(string materialName)
        {
            string sql = "SELECT * FROM Material WHERE ProductName LIKE '%{0}%' ORDER BY CreateTime ASC";
            sql = string.Format(sql, materialName);
            SqlDataReader reader = SQLHelper.GetReader(sql);
            

            List<Material> materials = new List<Material>();

            while (reader.Read())
            {
                materials.Add(new Material()
                {
                    MaterialId = reader["MaterialId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Img = reader["Img"].ToString(),
                    CreateTime = Convert.ToDateTime((reader["CreateTime"] == null) ? null : reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime((reader["CreateTime"] == null) ? null : reader["ModifyTime"]),
                    State = Convert.ToInt16(reader["State"])
                });
            }

            return materials;
        }

        /// <summary>
        /// 根据标准物名称模糊查询标准物集合
        /// </summary>
        /// <param name="materialName">专业名称</param>
        /// <returns></returns>
        public List<Material> GetMaterialsByCategory(string categoryName)
        {
            string sql = "SELECT * FROM Material WHERE CategoryName = '{0}' AND State = 1 ORDER BY CreateTime ASC";
            sql = string.Format(sql, categoryName);
            SqlDataReader reader = SQLHelper.GetReader(sql);

            List<Material> materials = new List<Material>();

            while (reader.Read())
            {
                materials.Add(new Material()
                {
                    MaterialId = reader["MaterialId"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Img = reader["Img"].ToString(),
                    CreateTime = Convert.ToDateTime((reader["CreateTime"] == null) ? null : reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime((reader["CreateTime"] == null) ? null : reader["ModifyTime"]),
                    State = Convert.ToInt16(reader["State"])
                });
            }

            return materials;
        }

        /// <summary>
        /// 获取标准物列表
        /// </summary>
        /// <returns>集合，包括标准ID，名称，图片，状态</returns>
        public List<Material> GetMaterials(bool state = false)
        {
            List<Material> materials = new List<Material>();

            string sql = "SELECT * FROM Material";
            if(state){
                sql += " WHERE State = 1";
            }

            sql += " ORDER BY CreateTime ASC";

            SqlDataReader reader = SQLHelper.GetReader(sql);

            while (reader.Read())
            {
                materials.Add(new Material()
                {
                    MaterialId = (reader["MaterialId"] == null) ? "" : reader["MaterialId"].ToString(),
                    ProductName = (reader["ProductName"] == null) ? "" : reader["ProductName"].ToString(),
                    Img = (reader["Img"] == null) ? "" : reader["Img"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                    State = Convert.ToInt16(reader["State"])
                });
            }

            return materials;
        }

        /// <summary>
        /// 根据id获取图片名称
        /// </summary>
        /// <param name="name">图片名称</param>
        /// <returns></returns>
        public string GetImgPath(string id)
        {
            string sql = "SELECT Img FROM Material WHERE MaterialId = '{0}'";
            sql = string.Format(sql, id);

            return (string)SQLHelper.GetSingleResult(sql);
        }


        public List<string> GetCategorys()
        {
            List<string> list = new List<string>();

            string sql = "SELECT DISTINCT(CategoryName) FROM Material ORDER BY CategoryName";

            SqlDataReader reader = SQLHelper.GetReader(sql);

            while (reader.Read())
            {
                list.Add(reader["CategoryName"].ToString());
            }

            return list;
        }

        public string GetCategoryId(string categoryName)
        {
            string sql = "SELECT CategoryId FROM Material WHERE CategoryName = '{0}'";
            sql = string.Format(sql, categoryName);

            string id = (string)SQLHelper.GetSingleResult(sql);

            return id;
        }
    }
}
