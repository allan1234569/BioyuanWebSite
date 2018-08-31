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
        //[Required(ErrorMessage = "{0}产品名称不能为空")]
        public string ProductName { get; set; }
        
        /// <summary>
        /// 项目列表
        /// </summary>
        public List<MaterialProject> materialProjects { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        //[Required(ErrorMessage = "{0}产品名称不能为空")]
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
        /// 推荐浓度
        /// </summary>
        public string RecommendedConcentration { get; set; }

        /// <summary>
        /// 稳定性
        /// </summary>
        public string Stability { get; set; }

        /// <summary>
        /// 产品特征
        /// </summary>
        public string Feature { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; set; }

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
        public int State { get; set; }




    }
}
