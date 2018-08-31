using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// 标准物质规格类
    /// </summary>
    public class MaterialSpecification
    {
        /// <summary>
        /// 规格ID
        /// </summary>
        public string SpecificationId { get; set; }

        /// <summary>
        /// 标准物项目ID
        /// </summary>
        public string MaterialProjectId { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 标准及不确定度
        /// </summary>
        public string StardardUncertairty { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 标准物质编号
        /// </summary>
        public string CertificateNo { get; set; }
    }
}
