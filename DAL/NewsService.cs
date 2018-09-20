using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Models;
using DAL.Helper;

namespace DAL
{
    public class NewsService
    {
        public int InsertNews(News news)
        {
            string sql = "INSERT INTO News(Id, F_Id, F_Name, Title, Author, KeyWord, Content, DateTime, Remark, Enable) VALUES";
            sql += "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', {9})";
            sql = string.Format(sql,
                news.id,
                news.f_id,
                news.f_name,
                news.title,
                news.author,
                news.keyword,
                news.content,
                news.dateTime,
                news.remark,
                news.enable);

            return SQLHelper.Update(sql);
        }

        public int UpdateNews(News news)
        {
            string sql = "UPDATE News SET F_Id = '{1}', F_Name = '{2}', Title = '{3}', Author = '{4}', KeyWord = '{5}', Content = '{6}', DateTime = '{7}', Remark = '{8}' WHERE Id = '{0}'";
            sql = string.Format(sql,
                news.id,
                news.f_id,
                news.f_name,
                news.title,
                news.author,
                news.keyword,
                news.content,
                news.dateTime,
                news.remark,
                news.enable
                );
            return 0;
        }



        public int DeleteNews(string id)
        {
            string sql = "DELETE FROM News WHERE Id = '{0}'";
            sql = string.Format(sql, id);

            return SQLHelper.Update(sql);
        }

        public News GetNewsDetail(string id)
        {
            string sql = "SELECT * FROM News WHERE Id = '{0}'";
            sql = string.Format(sql, id);

            News news = null;

            SqlDataReader reader = SQLHelper.GetReader(sql);

            if (reader.Read())
            {
                news = new News()
                {
                    id = reader["Id"].ToString(),
                    f_id = reader["F_Id"].ToString(),
                    f_name = reader["F_Name"].ToString(),
                    title = reader["Title"].ToString(),
                    author = reader["Author"].ToString(),
                    keyword = reader["KeyWord"].ToString(),
                    content = reader["Content"].ToString(),
                    dateTime = Convert.ToDateTime(reader["DateTime"]),
                    remark = reader["Remark"].ToString(),
                    enable = Convert.ToInt16(reader["Enable"])
                };
            }

            return news;
        }

        public List<News> GetNews(string title)
        {
            List<News> news = new List<News>();

            string sql = "SELECT * FROM News";
            if (title != string.Empty)
            {
                sql += "WHERE Title = '{0}'";
                sql = string.Format(sql, title);
            }

            sql += " ORDER BY DateTime ASC, Title ASC";
            
            SqlDataReader reader = SQLHelper.GetReader(sql);

            while (reader.Read())
            {
                News myNews = new News()
                {
                    id = reader["Id"].ToString(),
                    f_id = reader["F_Id"].ToString(),
                    f_name = reader["F_Name"].ToString(),
                    title = reader["Title"].ToString(),
                    author = reader["Author"].ToString(),
                    keyword = reader["KeyWord"].ToString(),
                    content = reader["Content"].ToString(),
                    dateTime = Convert.ToDateTime(reader["DateTime"]),
                    remark = reader["Remark"].ToString(),
                    enable = Convert.ToInt16(reader["Enable"])
                };

                news.Add(myNews);
            } 

            return news;
        }

        public int ReleaseNews(string id)
         {
            if (id == null || string.Empty == id)
            {
                return -1;
            }

            string sql = "UPDATE News set Enable = 1 where id = '{0}'";
            sql = string.Format(sql, id);

            return SQLHelper.Update(sql);
        }

    }
}
