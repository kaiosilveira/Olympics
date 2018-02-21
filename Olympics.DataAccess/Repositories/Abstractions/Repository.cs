using Olympics.DataAccess.Factories;
using Olympics.Domain.Contracts.Enumerators;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Olympics.DataAccess.Repositories.Abstractions
{
    public abstract class Repository
    {
        private TReturn _Dispatch<TReturn>(Func<IDbConnection, TReturn> query)
        {
            using (var con = ConnectionFactory.GetConnection(ConnectionStrings.Olympics))
            {
                return query(con);
            }
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            return this._Dispatch((con) => con.Query<T>(sql));
        }

        public IEnumerable<T> Query<T>(string sql, object parameters)
        {
            return this._Dispatch((con) => con.Query<T>(sql, parameters));
        }

        public T QuerySingle<T>(string sql, object parameters)
        {
            return this._Dispatch((con) => con.QuerySingle<T>(sql, parameters));
        }
       
    }

}
