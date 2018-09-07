using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;

namespace BLL
{
    public class ProductCategoryManager
    {

        public int InsertProductCategory(ProductCategory category)
        {
            return new ProductCategoryService().InsertProductCategory(category);
        }

        public int DeleteProductCategoryById(string id)
        {
            return new ProductCategoryService().DeleteProductCategoryById(id);
        }

        public int EnableProductCategory(string id)
        {
            return new ProductCategoryService().EnableProductCategory(id);
        }

        public int UpdateProductCategory(ProductCategory category)
        {
            return new ProductCategoryService().UpdateProductCategory(category);
        }

        public int GetProductCategoryState(string id)
        {
            return new ProductCategoryService().GetProductCategoryState(id);
        }

        public ProductCategory GetProductCategoryDetail(string id)
        {
            return new ProductCategoryService().GetProductCategoryDetail(id);
        }

        public List<ProductCategory> GetAllProductCategorys()
        {
            return new ProductCategoryService().GetAllProductCategorys();
        }

        public List<ProductCategory> GetProductCategorys(string categoryName)
        {
            return new ProductCategoryService().GetProductCategorys(categoryName);
        }

        public string GetProductCategoryId(string categoryName)
        {
            return new ProductCategoryService().GetProductCategoryId(categoryName);
        }

        public bool ProductCategoryExists(string name)
        {
            return new ProductCategoryService().ProductCategoryExists(name);
        }
    }
}
