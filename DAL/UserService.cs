using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models;
using DAL.Helper;

namespace DAL
{

    /// <summary>
    /// 用户查询类
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetUsers()
        {
            string sql = "select * from T_User ORDER BY CreateTime ASC";

            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<UserInfo> list = new List<UserInfo>();

            while (reader.Read())
            {
                list.Add(new UserInfo()
                {
                    UserId = reader["UserId"].ToString(),
                    NickName = reader["NickName"].ToString(),
                    LoginName = reader["LoginName"].ToString(),
                    UserEmail = reader["UserEmail"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                    State = Convert.ToInt32(reader["State"])
                });
            }

            return list;
        }

        /// <summary>
        /// 根据登录名查询
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserInfo GetUser(UserInfo user)
        {
            string sql = "SELECT * FROM T_User";
            sql += " WHERE LoginName = '{0}';";
            sql = string.Format(sql, 
                user.LoginName);

            SqlDataReader reader = SQLHelper.GetReader(sql);
            UserInfo admin = null;

            if (reader.Read())   //获取第一条记录
            {
                admin = new UserInfo();
                admin.UserId = reader["UserId"].ToString();
                admin.NickName = reader["NickName"].ToString();
                admin.LoginName = reader["LoginName"].ToString();
                admin.LoginPwd = reader["LoginPwd"].ToString();
                admin.UserEmail = reader["UserEmail"].ToString();
                admin.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                admin.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                admin.State = Convert.ToInt32(reader["State"]);
            }

            return admin;
        }

        public int GetId(string email)
        {
            string sql = "SELECT Id From T_USER WHERE UserEmail = '{0}';";
            sql = string.Format(sql, email);

            int id = -1;

            SqlDataReader reader = SQLHelper.GetReader(sql);
            if (reader.HasRows && reader.Read())
            {
                id = Convert.ToInt16(reader["Id"]);
            }
            else
            {
                id = -1;
            }

            return id;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int Login(UserInfo user)
        {
            if(GetUser(user) == null)
            {
                return -1;
            }

            string sql = "SELECT * FROM T_User";
            sql += " WHERE LoginName = '{0}' AND LoginPwd = '{1}';";
            sql = string.Format(sql,
                user.LoginName,
                user.LoginPwd);

            SqlDataReader reader = SQLHelper.GetReader(sql);
            UserInfo admin = null;

            if (reader.Read())   //获取第一条记录
            {
                admin = new UserInfo();
                admin.UserId = reader["UserId"].ToString();
                admin.NickName = reader["NickName"].ToString();
                admin.LoginName = reader["LoginName"].ToString();
                admin.LoginPwd = reader["LoginPwd"].ToString();
                admin.UserEmail = reader["UserEmail"].ToString();
                admin.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                admin.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                admin.State = Convert.ToInt32(reader["State"]);
            }

            if(admin == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }


        public bool EmailExists(string strMail)
        {
            if (strMail == string.Empty || strMail == null)
            {
                return false;
            }

            string sql = "SELECT * FROM T_User";
            sql += " WHERE UserEmail = '{0}';";

            sql = string.Format(sql, strMail);

            SqlDataReader reader = SQLHelper.GetReader(sql);
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LoginNameExists(string loginName)
        {
            if (loginName == string.Empty || loginName == null)
            {
                return false;
            }

            string sql = "SELECT * FROM T_User";
            sql += " WHERE LoginName = '{0}';";

            sql = string.Format(sql, loginName);

            SqlDataReader reader = SQLHelper.GetReader(sql);
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 根据用户名查询
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public List<UserInfo> GetUserByLoginName(String LoginName)
        {
            string sql = "select * from T_User";

            if (LoginName != null 
                && string.Empty != LoginName 
                && string.Empty != LoginName.Trim())
            {
                sql += " where UserName = '{0}'";
                sql = string.Format(sql, LoginName);
            }

            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<UserInfo> list = new List<UserInfo>();

            while (reader.Read())
            {
                list.Add(new UserInfo()
                {
                    UserId = reader["UserId"].ToString(),
                    NickName = reader["NickName"].ToString(),
                    LoginName = reader["LoginName"].ToString(),
                    UserEmail = reader["UserEmail"].ToString(),
                    CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                    ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                    State = Convert.ToInt32(reader["State"])
                });
            }

            return list;
        }

        /// <summary>
        ///  根据ID获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetUserById(String id)
        {
            string sql = "select * from T_User";

            if (id != null
                && string.Empty != id
                && string.Empty != id.Trim())
            {
                sql += " where UserId = '{0}'";
                sql = string.Format(sql, id);
            }

            SqlDataReader reader = SQLHelper.GetReader(sql);
            UserInfo admin = null;

            if (reader.Read())   //获取第一条记录
            {
                admin = new UserInfo();
                admin.UserId = reader["UserId"].ToString();
                admin.NickName = reader["NickName"].ToString();
                admin.LoginName = reader["LoginName"].ToString();
                admin.LoginPwd = reader["LoginPwd"].ToString();
                admin.UserEmail = reader["UserEmail"].ToString();
                admin.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                admin.ModifyTime = Convert.ToDateTime(reader["ModifyTime"]);
                admin.State = Convert.ToInt32(reader["State"]);
            }

            return admin;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// </returns>
        public RegisterType AddUser(UserInfo user)
        {
            //查询用户是否已经存在
            UserInfo adminUser = GetUser(user);

            if (adminUser != null)
            {
                return RegisterType.用户已存在;
            }

            string sql = "insert into T_User(UserId, NickName, LoginName, LoginPwd, UserEmail, CreateTime, ModifyTime, State, UserRank) Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8})";
            sql = string.Format(sql, user.UserId, user.NickName, user.LoginName, user.LoginPwd, user.UserEmail, user.CreateTime, user.ModifyTime, user.State, user.UserRank);

            int ret = SQLHelper.Update(sql);

            if (ret == -1)
            {
                return RegisterType.注册失败;
            }
            else
            {
                return RegisterType.注册成功;
            }
        }

        /// <summary>
        ///  删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUserById(string id)
        {
            if( id != null && string.Empty == id)
            {
                return -1;
            }

            string sql = "delete from T_User";
            sql += " where UserId = '{0}'";
            sql = string.Format(sql, id);

            return SQLHelper.Update(sql);
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="LoginPwd"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        public int UpdateUser(UserInfo user)
        {
            string sql = "UPDATE T_User SET NickName = '{1}', LoginName = '{2}' , LoginPwd = '{3}', ModifyTime = '{4}' WHERE AdminId = '{0}'";
            sql = string.Format(sql, 
                user.UserId,
                user.NickName,
                user.LoginName,
                user.LoginPwd,
                user.ModifyTime);

            return SQLHelper.Update(sql);
        }


        /// <summary>
        /// 获取用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUserState(string id)
        {
            string sql = "select State from T_User";
            sql += " where UserId = '{0}'";
            sql = string.Format(sql, id);

            int state = Convert.ToInt16(SQLHelper.GetSingleResult(sql));

            return state;
        }

        /// <summary>
        /// 开启/关闭用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EnableUser(string id)
        {
            if (id == null || string.Empty == id)
            {
                return -1;
            }
            string sql = "select State from T_User";
            sql += " where UserId = '{0}'";
            sql = string.Format(sql, id);

            int state = Convert.ToInt16(SQLHelper.GetSingleResult(sql));

            if (state == 0)
            {
                state = 1;
            }
            else
            {
                state = 0;
            }

            sql = "update T_User set State = '{0}' where UserId = '{1}'";
            sql = string.Format(sql, state, id);

            int retVal = SQLHelper.Update(sql);

            if(retVal > 0)
            {
                return state;
            }

            return -1;
        }

        public bool ModifyPassword(UserInfo user)
        {
            if (user.LoginPwd == null || user.LoginPwd == string.Empty)
            {
                return false;
            }

            string sql = "update T_User set LoginPwd = '{0}' where UserEmail = '{1}';";
            sql = string.Format(sql, user.LoginPwd, user.UserEmail);

            int retVal = SQLHelper.Update(sql);

            if (retVal > 0)
            { 
                return true;
            }
            else
            {
                return false;
            }
        }



        public Nullable<DateTime> GetLostPasswordTime(string id)
        {

            Nullable<DateTime> dt = null;

            if(id != null && id != string.Empty)
            {
                string sql = "select CTime from LostPwd where User_Id = '{0}'";
                sql = string.Format(sql, id);

                SqlDataReader reader = SQLHelper.GetReader(sql);

                if (reader.HasRows && reader.Read())
                {
                    dt = Convert.ToDateTime(reader["CTime"]);
                }
                else
                {
                    dt = null;
                }
            }

            return dt;
        }

    }
}
