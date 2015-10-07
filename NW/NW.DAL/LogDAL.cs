﻿using NW.Entity;
using NW.Factory;
using NW.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace NW.DAL
{
    public class LogDAL : IBaseDAL<Log>, ILogDAL
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
                string query = "DELETE FROM Logs WHERE Id = @Id";
                return Conn.Execute(query, new { Id = id });
            }
        }

        public int Delete(Log model)
        {
            using (Conn)
            {
                string query = "DELETE FROM Logs WHERE Id = @Id";
                return Conn.Execute(query, model);
            }
        }

        public Log GetEntity(int id)
        {
            Log log;
            string query = "SELECT * FROM Logs WHERE Id = @Id";
            using (Conn)
            {
                log = Conn.Query<Log>(query, new { Id = id }).SingleOrDefault();
                return log;
            }
        }

        public Log GetEntityWithRefence(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Log> GetList(string whereStr)
        {
            using (Conn)
            {
                string query = "";
                if (!string.IsNullOrEmpty(whereStr))
                {
                    query = "SELECT * FROM Logs where " + whereStr + " order by Time desc";
                }
                else
                {
                    query = "SELECT * FROM Logs order by Time desc";
                }

                return Conn.Query<Log>(query);
            }
        }

        public IEnumerable<Log> GetListByPage(int page, int size, string whereStr)
        {
            int index = size * (page - 1);
            using (Conn)
            {
                string query = "";
                if (!string.IsNullOrEmpty(whereStr))
                {
                    query = "SELECT * FROM Logs where " + whereStr + " order by Time desc limit " + index + "," + size;
                }
                else
                {
                    query = "SELECT * FROM Logs order by Time desc limit " + index + "," + size;
                }
                return Conn.Query<Log>(query);
            }
        }

        public int Insert(Log model)
        {
            using (Conn)
            {
                string query = "INSERT INTO Logs (Time,Thread,Level,Type,Description,Exception,Ip) VALUES (@Time,@Thread,@Level,@Type,@Description,@Exception,@Ip)";
                return Conn.Execute(query, model);
            }
        }

        public int Update(Log model)
        {
            using (Conn)
            {
                string query = "UPDATE Logs SET  Time=@Time,Thread=@Thread,Level=@Level,Type=@Type,Description=@Description,Exception=@Exception,Ip=@Ip WHERE Id =@Id";
                return Conn.Execute(query, model);
            }
        }
    }
}
