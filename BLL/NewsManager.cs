using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL;

namespace BLL
{
    public class NewsManager
    {
        public int InsertNews(News news)
        {
            news.f_id = new NewsCategoryManager().GetNewsCategoryId(news.f_name);

            return new NewsService().InsertNews(news);
        }

        public int UpdateNews(News news)
        {
            return new NewsService().UpdateNews(news);
        }

        public int DeleteNews(string id)
        {
            return new NewsService().DeleteNews(id);
        }

        public News GetNewsDetail(string id)
        {
            return new NewsService().GetNewsDetail(id);
        }

        public List<News> GetNews(string title)
        {
            return new NewsService().GetNews(title);
        }

        public int ReleaseNews(string id)
        {
            return new NewsService().ReleaseNews(id);
        }

    }
}
