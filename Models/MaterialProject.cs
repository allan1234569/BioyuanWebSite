using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Models
{
    public class MaterialProject
    {
        /// <summary>
        /// 标准物质检测项目ID
        /// </summary>
        public string materialProjectId { get; set; }

        /// <summary>
        /// 标准物质检测项目
        /// </summary>
        public string materialProjectName { get; set; }

        /// <summary>
        /// 标准物质ID
        /// </summary>
        public string materialId { get; set; }
        
        /// <summary>
        /// 标准物质检测项目单位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 标准物质检测项目规格列表
        /// </summary>
        public List<MaterialSpecification> materialSpecifications { get; set; }

    }
}
