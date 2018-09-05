using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL;
namespace BLL
{


    /// <summary>
    /// 用户管理类
    /// </summary>
    public class UserManager
    {
        /// <summary>
        /// 登录管理
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// -1  用户不存在
        /// 0   密码错误
        /// </returns>
        //public int Login(UserInfo user)
        //{
        //    string pwd = user.LoginPwd;

        //    user.LoginPwd = new AEncryption().Encryption(user.LoginPwd);

        //    return new UserService().Login(user);
        //}

        public UserInfo Login(UserInfo user)
        {
            return new UserService().Login(user);
        }

        /// <summary>
        /// 登录名查询管理
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public UserInfo GetUser(UserInfo user)
        {
            UserInfo tUser = new UserService().GetUser(user);

            return tUser;
        }

        /// <summary>
        /// 登录名查询管理
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public List<UserInfo> GetUserByLoginName(string LoginName)
        {
            return new UserService().GetUserByLoginName(LoginName);
        }

        public UserInfo GetUserById(String id)
        {
            return new UserService().GetUserById(id);
        }


        /// <summary>
        /// 获取所有用户管理
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetUsers()
        {
            return new UserService().GetUsers();
        }


        public int GetId(string email)
        {
            return new UserService().GetId(email);
        }

        public bool EmailExists(string strMail)
        {
            return new UserService().EmailExists(strMail);
        }

        public bool LoginNameExists(string loginName)
        {
            return new UserService().LoginNameExists(loginName);
        }

        /// <summary>
        /// 添加用户管理
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RegisterType AddUser(UserInfo user)
        {

            user.UserId = new Common().getGUID();

            user.CreateTime = DateTime.Now;

            user.ModifyTime = DateTime.Now;

            return new UserService().AddUser(user);
        }

        public int DeleteUserById(string id)
        {
            return new UserService().DeleteUserById(id);
        }

        public int ModifyUser(UserInfo user, string originPwd)
        {
            UserService us = new UserService();

            return us.UpdateUser(user);
        }

        public int EnableUser(string id)
        {
            return new UserService().EnableUser(id);
        }

        public int GetUserState(string id)
        {
            return new UserService().GetUserState(id);
        }

        public bool ModifyPassword(UserInfo user)
        {
            return new UserService().ModifyPassword(user);
        }

        public bool ResetpwdUrlValid(string id)
        {
            string dt_now = DateTime.Now.ToString();

            string dt_ori = new UserService().GetLostPasswordTime(id).ToString();

            if (string.Empty != dt_now && string.Empty != dt_ori)
            {
                TimeSpan ts = DateTime.Parse(dt_now) - DateTime.Parse(dt_ori);

                if (ts.Minutes > 5)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }


        }
    }
}
