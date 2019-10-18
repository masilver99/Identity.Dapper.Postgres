using System.Data;
using System.Threading.Tasks;

namespace Identity.Dapper.Postgres
{
    public interface IDatabaseConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}
