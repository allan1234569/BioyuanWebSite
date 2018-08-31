using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class NewsCategory : Category
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string Description { get; set; }
    }
}
