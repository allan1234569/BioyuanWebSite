using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Category
    {
        /// <summary>
        /// 分类编号
        /// </summary>
        public string CategoryId { get; set; }

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
