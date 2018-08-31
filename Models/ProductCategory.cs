using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class ProductCategory : Category
    {
        /// <summary>
        /// 专业名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 专业描述
        /// </summary>
        public string Description { get; set; }
    }
}
