using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Models
{
    /// <summary>
    /// 标准物质类
    /// </summary>
    public class Material
    {
        /// <summary>
        /// ID
        /// </summary>
        public string MaterialId { get; set; }

        
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品描述(用途)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 专业ID
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 浓度水平
        /// </summary>
        public string Concentration { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string SingleSpecification { get; set; }


        /// <summary>
        /// 包装规格
        /// </summary>
        public string PackingSpecification { get; set; }

        /// <summary>
        /// 产品状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 储存条件
        /// </summary>
        public string StorageCondition { get; set; }

        /// <summary>
        /// 效期
        /// </summary>
        public string UsefulLife { get; set; }

        /// <summary>
        /// 保存稳定性
        /// </summary>
        public string PreservationStability { get; set; }

        /// <summary>
        /// 产品基质
        /// </summary>
        public string ProductMatrix { get; set; }

        /// <summary>
        ///包含项目
        /// </summary>
        public string ContainedItems { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public int Enable { get; set; }




    }
}
