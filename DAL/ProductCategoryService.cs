﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models;
using DAL.Helper;

namespace DAL
{
    public class ProductCategoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int InsertProductCategory(ProductCategory category)
        {
            string sql = "INSERT INTO ProductCategory(CategoryId, CategoryName, Description, CreateTime, ModifyTime, State) VALUES('{0}', '{1}', '{2}', '{3}', {4});";
            sql = string.Format(sql,
                category.CategoryId,
                category.CategoryName,
                category.Description,
                category.CreateTime,
                category.ModifyTime,
                category.State);

            return SQLHelper.Update(sql);
        }

        public int DeleteProductCategoryById(string id)
        {
            string sql = "DELETE FROM ProductCategory WHERE CategoryId = '{0}'";
            sql = string.Format(sql, id);

            return SQLHelper.Update(sql);
        }

        public int EnableProductCategory(string id)
        {
            if (id == null || string.Empty == id)
            {
                return -1;
            }

            int state = GetProductCategoryState(id);

            string sql = "UPDATE ProductCategory SET State = '{1}' WHERE CategoryId = '{0}';";
            
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

        public int UpdateProductCategory(ProductCategory category)
        {
            string sql = "UPDATE ProductCategory SET CategoryName = '{1}', Description = '{2}', ModifyTime = '{3}' WHERE CategoryId = '{0}';";
            sql = string.Format(sql,
                category.CategoryId,
                category.CategoryName,
                category.Description,
                category.ModifyTime);

            return SQLHelper.Update(sql);
        }

        public int GetProductCategoryState(string id)
        {
            string sql = "SELECT State FROM ProductCategory WHERE CategoryId = '{0}';";
            sql = string.Format(sql, id);

            int state = Convert.ToInt16(SQLHelper.GetSingleResult(sql));

            return state;
        }


        public ProductCategory GetProductCategoryDetail(string id)
        {
            string sql = "SELECT * FROM ProductCategory WHERE CategoryId = '{0}'";
            sql = string.Format(sql, id);

            SqlDataReader reader = SQLHelper.GetReader(sql);

            ProductCategory category = null;

            if (reader.Read())
            {
                category = new ProductCategory()
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


        public List<ProductCategory> GetAllProductCategorys()
        {
            string sql = "SELECT * FROM ProductCategory ORDER BY CreateTime";

            SqlDataReader reader = SQLHelper.GetReader(sql);

            List<ProductCategory> categorys = new List<ProductCategory>();

            while (reader.Read())
            {
                categorys.Add(new ProductCategory(){
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


        public List<ProductCategory> GetProductCategorys(string categoryName)
        {
            string sql = "SELECT * FROM ProductCategory WHERE CategoryName LIKE '%{0}%' ORDER BY CreateTime";
            sql = string.Format(sql, categoryName);

            SqlDataReader reader = SQLHelper.GetReader(sql);

            List<ProductCategory> categorys = new List<ProductCategory>();

            while (reader.Read())
            {
                categorys.Add(new ProductCategory()
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


        public string GetProductCategoryId(string categoryName)
        {
            string sql = "SELECT CategoryId FROM ProductCategory WHERE CategoryName = '{0}'";
            sql = string.Format(sql, categoryName);

            string id = (string)SQLHelper.GetSingleResult(sql);

            return id;
        }

    }
}