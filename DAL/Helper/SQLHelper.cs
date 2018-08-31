using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL.Helper
{
    /// <summary>
    /// 通用数据访问类
    /// </summary>
    public class SQLHelper
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
        

        /// <summary>
        /// 执行增、删、改的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlTransaction myTran = conn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn, myTran);
                //cmd.Transaction = myTran;

                int ret = cmd.ExecuteNonQuery();
                myTran.Commit();//成功提交

                return ret;
            }
            catch (Exception ex)
            {
                myTran.Rollback();//出错回滚

                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 执行单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }


        /// <summary>
        /// 执行一个结果集的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw ex;
            }
        }
    }
}
