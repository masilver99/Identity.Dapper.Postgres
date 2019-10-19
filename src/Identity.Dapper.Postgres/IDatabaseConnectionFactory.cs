using System.Data;
using System.Threading.Tasks;

namespace Identity.Dapper.Postgres
{
    /// <summary>
    /// Responsible for returning database connection
    /// </summary>
    public interface IDatabaseConnectionFactory
    {
        /// <summary>
        /// Returns Database Connection
        /// </summary>
        /// <returns></returns>
        Task<IDbConnection> CreateConnectionAsync();
    }
}
