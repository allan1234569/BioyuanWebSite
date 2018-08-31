using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL;

namespace BLL
{
    /// <summary>
    /// 标准物质管理类
    /// </summary>
    public class MaterialManager
    {
        /// <summary>
        /// 添加标准物质
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public int InsertMaterial(Material material)
        {
            material.CategoryId = new ProductCategoryManager().GetProductCategoryId(material.CategoryName);

            return new MaterialService().InsertMaterial(material);
        }


        /// <summary>
        /// 删除标准物质
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public int DeleteMaterialById(string materialId)
        {
            string img_name = GetImgPath(materialId);

            int ret = new MaterialService().DeleteMaterialById(materialId);

            if (ret > 0)
            {
                new Common().DeleteFile(img_name, fileType.ImageType);
            }
            
            return ret;
        }

        public int UpdateMaterialById(Material material)
        {
            string oldImageName = GetImgPath(material.MaterialId);

            material.CategoryId = new ProductCategoryManager().GetProductCategoryId(material.CategoryName);

            int ret = new MaterialService().UpdateMaterialById(material);

            if (ret > 0)
            {
                new Common().DeleteFile(oldImageName, fileType.ImageType);
            }

            return ret;
        }


        /// <summary>
        /// 启用标准物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EnableMaterial(string id)
        {
            return new MaterialService().EnableMaterial(id);
        }

        public Material GetMaterialDetail(string id)
        {
            return new MaterialService().GetMaterialDetail(id);
        }

        public int UpdateMaterial(Material material)
        {
            bool deleteFlag = true;

            string oldImageName = GetImgPath(material.MaterialId);

            if (material.Img == String.Empty)
            {
                material.Img = oldImageName;
                deleteFlag = false;
            }

            material.CategoryId = GetCategoryId(material.CategoryName);
            int ret = new MaterialService().UpdateMaterial(material);

            if (ret > 0 && deleteFlag)
            {
                new Common().DeleteFile(oldImageName, fileType.ImageType);
            }

            return ret;
        }

        public List<Material> GetMaterials(string materialName)
        {
            return new MaterialService().GetMaterials(materialName);
        }

        public List<Material> GetMaterialsByCategory(string categoryName)
        {
            return new MaterialService().GetMaterialsByCategory(categoryName);
        }

        /// <summary>
        /// 获取标准物质集合
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public List<Material> GetMaterials(bool state = false)
        {
            return new MaterialService().GetMaterials(state);
        }

        /// <summary>
        /// 根据id获取图片名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetImgPath(string id)
        {
            return new MaterialService().GetImgPath(id);
        }

        public List<string> GetCategorys()
        {
            return new MaterialService().GetCategorys();
        }

        public string GetCategoryId(string categoryName)
        {
            return new MaterialService().GetCategoryId(categoryName);
        }
    }
}
