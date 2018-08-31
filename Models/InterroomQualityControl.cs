using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// 室间质评品类
    /// </summary>
    public class InterroomQualityControl
    {
        /// <summary>
        /// ID
        /// </summary>
        public string InterroomQualityControlId { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品描述
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
        ///分析物
        /// </summary>
        public string Analyte { get; set; }

        /// <summary>
        /// 产品组成
        /// </summary>
        public string Constitute { get; set; }

        /// <summary>
        /// 单支规格
        /// </summary>
        public string SingleSpecification { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 产品特征
        /// </summary>
        public string Feature { get; set; }

        /// <summary>
        /// 保存条件
        /// </summary>
        public string StorageCondition { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string UsefulLife { get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
        public string CertificateNo { get; set; }

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
