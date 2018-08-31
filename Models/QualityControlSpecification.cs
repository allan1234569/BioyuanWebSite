using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// 质控品规格类
    /// </summary>
    public class QualityControlSpecification
    {
        /// <summary>
        /// 规格ID
        /// </summary>
        public string SpecificationId { get; set; }

        /// <summary>
        /// 质控品ID
        /// </summary>
        public string QualityControlId { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 浓度
        /// </summary>
        public string Concentration { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }
    }
}
