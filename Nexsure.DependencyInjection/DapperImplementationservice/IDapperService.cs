using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.DependencyInjection.DapperImplementationservice
{
    public interface IDapperService
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, CommandType cmdType = CommandType.Text);
        Task<int> ExecuteAsync(string sql, object parameters = null, CommandType cmdType = CommandType.Text);
        Task<IEnumerable<T>> FilterAsync<T>(string sql, Expression<Func<T, bool>> predicate, object parameters = null);
        
    }
}
