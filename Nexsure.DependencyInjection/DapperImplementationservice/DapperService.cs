using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.DependencyInjection.DapperImplementationservice
{
    public class DapperService : IDapperService
    {
        private readonly SqlConnection _connection;

        public DapperService(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, CommandType cmdType = CommandType.Text)
        {
            return await _connection.QueryAsync<T>(sql, parameters, commandType: cmdType);
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null, CommandType cmdType = CommandType.Text)
        {
            return await _connection.ExecuteAsync(sql, parameters, commandType: cmdType);
        }

        public async Task<IEnumerable<T>> FilterAsync<T>(string sql, Expression<Func<T, bool>> predicate, object parameters = null)
        {
            var result = await _connection.QueryAsync<T>(sql, parameters);
            return result.AsQueryable().Where(predicate);
        }
    }
}
