using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;

namespace BLL
{
    public class NewsCategoryManager
    {

        public int InsertNewsCategory(NewsCategory category)
        {
            return new NewsCategoryService().InsertNewsCategory(category);
        }

        public int DeleteNewsCategoryById(string id)
        {
            return new NewsCategoryService().DeleteNewsCategoryById(id);
        }

        public int EnableNewsCategory(string id)
        {
            return new NewsCategoryService().EnableNewsCategory(id);
        }

        public int UpdateNewsCategory(NewsCategory category)
        {
            return new NewsCategoryService().UpdateNewsCategory(category);
        }

        public int GetNewsCategoryState(string id)
        {
            return new NewsCategoryService().GetNewsCategoryState(id);
        }

        public NewsCategory GetNewsCategoryDetail(string id)
        {
            return new NewsCategoryService().GetNewsCategoryDetail(id);
        }

        public List<NewsCategory> GetAllNewsCategorys()
        {
            return new NewsCategoryService().GetAllNewsCategorys();
        }

        public List<NewsCategory> GetNewsCategorys(string categoryName)
        {
            return new NewsCategoryService().GetNewsCategorys(categoryName);
        }

        public string GetNewsCategoryId(string categoryName)
        {
            return new NewsCategoryService().GetNewsCategoryId(categoryName);
        }

    }
}
