using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// ID
        /// </summary>
        public string AdminId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }

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

        ///// <summary>
        ///// 权限
        ///// </summary>
        //public int Limit { get; set; }

        //用户组：0代表管理员，1代表普通用户
        public int UserRank { get; set; }
    }
}
