using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL;

namespace BLL
{
    
    /// <summary>
    /// 室间质评品管理类
    /// </summary>
    public class InterroomQualityControlManager
    {
        public int InsertInterroomQualityControl(InterroomQualityControl qualityControl)
        {
            qualityControl.CategoryId = new ProductCategoryManager().GetProductCategoryId(qualityControl.CategoryName);

            return new InterroomQualityControlService().InsertInterroomQualityControl(qualityControl);
        }

        public int DeleteInterroomQualityControl(string qualityControlId)
        {
            string filename = GetImgPath(qualityControlId);

            int ret = new InterroomQualityControlService().DeleteInterroomQualityControl(qualityControlId);

            if (ret > 0)
            {
                new Common().DeleteFile(filename, fileType.ImageType);
            }

            return ret;
        }

        public int UpdateInterroomQualityControl(InterroomQualityControl qualityControl)
        {
            bool deleteFlag = true;

            string oldImageName = GetImgPath(qualityControl.InterroomQualityControlId);

            if(qualityControl.Img == String.Empty)
            {
                qualityControl.Img = oldImageName;
                deleteFlag = false;
            }

            qualityControl.CategoryId = new ProductCategoryManager().GetProductCategoryId(qualityControl.CategoryName);

            int ret = new InterroomQualityControlService().UpdateInterroomQualityControl(qualityControl);

            if (ret > 0 && deleteFlag)
            {
                new Common().DeleteFile(oldImageName, fileType.ImageType);
            }

            return ret;
        }

        public int EnableInterroomQualityControl(string id)
        {
            return new InterroomQualityControlService().EnableInterroomQualityControl(id);
        }

        public InterroomQualityControl GetInterroomQualityControlDetail(string id)
        {
            return new InterroomQualityControlService().GetInterroomQualityControlDetail(id);
        }

        public List<InterroomQualityControl> GetInterroomQualityControls(string qualityControlName)
        {
            return new InterroomQualityControlService().GetInterroomQualityControls(qualityControlName);
        }

        public List<InterroomQualityControl> GetInterroomQualityControlsByCategory(string categoryName)
        {
            return new InterroomQualityControlService().GetInterroomQualityControlsByCategory(categoryName);
        }

        public List<InterroomQualityControl> GetLaboratoryQuailtyControls(bool state = false)
        {
            return new InterroomQualityControlService().GetInterroomQualityControls(state);
        }

        public string GetImgPath(string id)
        {
            return new InterroomQualityControlService().GetImgPath(id);
        }

        public List<string> GetCategorys()
        {
            return new InterroomQualityControlService().GetCategorys();
        }
    }
}
