using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public enum RegisterType
    {
        注册成功 = 0,
        注册失败,
        用户已存在,
        用户名已经被使用过,
        用户名为空,
        检查通过,
        邮箱已经注册过,
        邮箱为空,
        未知错误
    }

    public enum LoginType
    {
        登录成功 = 0,
        用户名不存在,
        密码错误
    }

    public class UserInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户呢称
        /// </summary>
        public string NickName { get; set; }

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
        /// 邮箱
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 用户组：0代表管理员，1代表普通用户
        /// </summary>
        public int UserRank { get; set; }

    }
}
