using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL;

namespace BLL
{
    /// <summary>
    /// 室内质控品管理类
    /// </summary>
    public class LaboratoryQuailtyControlManager
    {
        public int InsertLaboratoryQuailtyControl(LaboratoryQuailtyControl qualityControl)
        {
            qualityControl.CategoryId = new ProductCategoryManager().GetProductCategoryId(qualityControl.CategoryName);

            return new LaboratoryQuailtyControlService().InsertLaboratoryQuailtyControl(qualityControl);
        }

        public int DeleteLaboratoryQuailtyControl(string qualityControlId)
        {
            string filename = GetImgPath(qualityControlId);

            int ret = new LaboratoryQuailtyControlService().DeleteLaboratoryQuailtyControl(qualityControlId);

            if (ret > 0)
            {
                new Common().DeleteFile(filename, fileType.ImageType);
            }

            return ret;
        }

        public int UpdateLaboratoryQuailtyControl(LaboratoryQuailtyControl qualityControl)
        {
            bool deleteFlag = true;
         
            string oldImageName = GetImgPath(qualityControl.LaboratoryQualityControlId);

            

            if (qualityControl.Img == String.Empty)
            {
                qualityControl.Img = oldImageName;
                deleteFlag = false;
            }

            qualityControl.CategoryId = new ProductCategoryManager().GetProductCategoryId(qualityControl.CategoryName);

            int ret =  new LaboratoryQuailtyControlService().UpdateLaboratoryQuailtyControl(qualityControl);

            if (ret > 0 && deleteFlag)
            {
                new Common().DeleteFile(oldImageName, fileType.ImageType);
            }

            return ret;
        }

        public int EnableLabratoryQualityControl(string id)
        {
            return new LaboratoryQuailtyControlService().EnableLabratoryQualityControl(id);
        }

        public LaboratoryQuailtyControl GetLaboratoryQuailtyControlDetail(string id)
        {
            return new LaboratoryQuailtyControlService().GetLaboratoryQuailtyControlDetail(id);
        }

        public List<LaboratoryQuailtyControl> GetLaboratoryQuailtyControls(string qualityControlName)
        {
            return new LaboratoryQuailtyControlService().GetLaboratoryQuailtyControls(qualityControlName);
        }

        public List<LaboratoryQuailtyControl> GetLaboratoryQuailtyControlsByCategory(string categoryName)
        {
            return new LaboratoryQuailtyControlService().GetLaboratoryQuailtyControlsByCategory(categoryName);
        }

        public List<LaboratoryQuailtyControl> GetLaboratoryQuailtyControls(bool state = false)
        {
            return new LaboratoryQuailtyControlService().GetLaboratoryQuailtyControls(state);
        }

        public string GetImgPath(string id)
        {
            return new LaboratoryQuailtyControlService().GetImgPath(id);
        }

        public List<string> GetCategorys()
        {
            return new LaboratoryQuailtyControlService().GetCategorys();
        }
    }
}
