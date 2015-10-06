﻿using NW.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NW.Entity;
using System.Data;
using NW.Factory;
using Dapper;

namespace NW.DAL
{
    public class ArticleDAL : IBaseDAL<Article>, IArticleDAL
    {

        #region 得到数据库链接对象
        private IDbConnection _conn;
        public IDbConnection Conn
        {
            get
            {
                return _conn = ConnectionFactory.CreateConnection();
            }
        }

        #endregion

        public int Delete(int id)
        {
            using (Conn)
            {
                string query = "DELETE FROM Article WHERE Id = @Id";
                return Conn.Execute(query, new { Id = id });
            }
        }

        public int Delete(Article model)
        {
            using (Conn)
            {
                string query = "DELETE FROM Article WHERE Id = @Id";
                return Conn.Execute(query, model);
            }
        }

        public Article GetEntity(int id)
        {
            Article article;
            string query = "SELECT * FROM Article WHERE Id = @Id";
            using (Conn)
            {
                article = Conn.Query<Article>(query, new { Id = id }).SingleOrDefault();
                return article;
            }
        }

        public Article GetEntityWithRefence(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Article> GetList()
        {
            using (Conn)
            {
                string query = "SELECT * FROM Article order by Time desc";
                return Conn.Query<Article>(query).ToList();
            }
        }

        public int Insert(Article model)
        {
            using (Conn)
            {
                string query = "INSERT INTO Article(Title,Description,Time,Browses,Category,UserId)VALUES(@Title,@Description,@Time,@Browses,@Category,@UserId)";
                return Conn.Execute(query, model);
            }
        }

        public int Update(Article model)
        {
            using (Conn)
            {
                string query = "UPDATE User SET  Title=@Title,Description=@Description,Time=@Time,Browses=@Browses,Category=@Category,UserId=@UserId WHERE Id =@Id";
                return Conn.Execute(query, model);
            }
        }

        public IEnumerable<Article> GetListByPage(int page, int size, string whereStr)
        {
            int index = size * (page - 1);
            using (Conn)
            {
                string query = "";
                if (!string.IsNullOrEmpty(whereStr))
                {
                    query = "SELECT * FROM Article where " + whereStr + " order by Time desc limit " + index + "," + size;
                }
                else
                {
                    query = "SELECT * FROM Article order by Time desc limit " + index + "," + size;
                }
                return Conn.Query<Article>(query);
            }
        }
    }
}