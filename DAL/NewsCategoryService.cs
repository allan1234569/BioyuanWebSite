using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL.Helper;
using Models;

namespace DAL
{
    public class NewsCategoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int InsertNewsCategory(NewsCategory category)
        {
            string sql = "INSERT INTO NewsCategory(CategoryId, CategoryName, Description, CreateTime, ModifyTime, State) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');";
            sql = string.Format(sql,
                category.CategoryId,
                category.CategoryName,
                category.Description,
                category.CreateTime,
                category.ModifyTime,
                category.State);

            return SQLHelper.Update(sql);
        }

        public int DeleteNewsCategoryById(string id)
        {
            string sql = "DELETE FROM NewsCategory WHERE CategoryId = '{0}'";
            sql = string.Format(sql, id);

            return SQLHelper.Update(sql);
        }

        public int EnableNewsCategory(string id)
        {
            if (id == null || string.Empty == id)
            {
                return -1;
            }

            int state = GetNewsCategoryState(id);

            string sql = "UPDATE NewsCategory SET State = '{1}' WHERE CategoryId = '{0}';";

            if (state == 0)
            {
                sql = string.Format(sql, id, 1);
            }
            else
            {
                sql = string.Format(sql, id, 0);
            }

            return SQLHelper.Update(sql);
        }

        public int UpdateNewsCategory(NewsCategory category)
        {
            string sql = "UPDATE NewsCategory SET CategoryName = '{1}', Description = '{2}', ModifyTime = '{3}' WHERE CategoryId = '{0}';";
            sql = string.Format(sql,
                category.CategoryId,
                category.CategoryName,
                category.Description,
                category.ModifyTime);

            return SQLHelper.Update(sql);
        }

        public int GetNewsCategoryState(string id)
        {
            string sql = "SELECT State FROM NewsCategory WHERE CategoryId = '{0}';";
            sql = string.Format(sql, id);

            int state = Convert.ToInt16(SQLHelper.GetSingleResult(sql));

            return state;
        }


        public NewsCategory GetNewsCategoryDetail(string id)
        {
            string sql = "SELECT * FROM NewsCategory WHERE CategoryId = '{0}'";
            sql = string.Format(sql, id);

            SqlDataReader reader = SQLHelper.GetReader(sql);

            NewsCategory category = null;

            if (reader.Read())
            {
                category = new NewsCategory()
                {
                    CategoryId = reader["CategoryId"].ToString(),
                    CategoryName = reader["CategoryName"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                    State = Convert.ToInt16(reader["State"])
                };
            }

            return category;
        }


        public List<NewsCategory> GetAllNewsCategorys()
        {
            string sql = "SELECT * FROM NewsCategory ORDER BY CreateTime";

            SqlDataReader reader = SQLHelper.GetReader(sql);

            List<NewsCategory> categorys = new List<NewsCategory>();

            while (reader.Read())
            {
                categorys.Add(new NewsCategory()
                {
                    CategoryId = reader["CategoryId"].ToString(),
                    CategoryName = reader["CategoryName"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["modifyTime"]),
                    State = Convert.ToInt16(reader["State"])
                });
            }

            return categorys;
        }


        public List<NewsCategory> GetNewsCategorys(string categoryName)
        {
            string sql = "SELECT * FROM NewsCategory WHERE CategoryName LIKE '%{0}%' ORDER BY CreateTime";
            sql = string.Format(sql, categoryName);

            SqlDataReader reader = SQLHelper.GetReader(sql);

            List<NewsCategory> categorys = new List<NewsCategory>();

            while (reader.Read())
            {
                categorys.Add(new NewsCategory()
                {
                    CategoryId = reader["CategoryId"].ToString(),
                    CategoryName = reader["CategoryName"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["modifyTime"]),
                    State = Convert.ToInt16(reader["State"])
                });
            }

            return categorys;
        }


        public string GetNewsCategoryId(string categoryName)
        {
            string sql = "SELECT CategoryId FROM NewsCategory WHERE CategoryName = '{0}'";
            sql = string.Format(sql, categoryName);

            string id = (string)SQLHelper.GetSingleResult(sql);

            return id;
        }

        public bool NewsCategoryExists(string name)
        {
            if (name == string.Empty || name == null)
            {
                return false;
            }

            string sql = "SELECT * FROM NewsCategory";
            sql += " WHERE CategoryName = '{0}';";

            sql = string.Format(sql, name);

            SqlDataReader reader = SQLHelper.GetReader(sql);
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
