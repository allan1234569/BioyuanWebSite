using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class LaboratorySpecification
    {
        /// <summary>
        /// 规格ID
        /// </summary>
        public string SpecificationId { get; set; }

        /// <summary>
        /// 室内质控品ID
        /// </summary>
        public string LaboratoryQualityControlId { get; set; }
        
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

        /// <summary>
        /// 证书编号
        /// </summary>
        public string CertificateNo { get; set; }
    }
}
