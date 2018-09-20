using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{

    public class News
    {
        /// <summary>
        /// 新闻编号
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 分类编号
        /// </summary>
        public string f_id { get; set; }

        /// <summary>
        /// 分类名
        /// </summary>
        public string f_name { get; set; }

        /// <summary>
        /// 新闻名
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 作者/发布人
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }

        public string content { get; set; }

        /// <summary>
        /// 新闻的日期
        /// </summary>
        public DateTime dateTime { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public int enable { get; set; }
    }
}
