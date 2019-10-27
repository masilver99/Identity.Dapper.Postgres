using System;
using System.Data;
using Npgsql;
using System.Threading.Tasks;
using D = Dapper;

namespace Identity.Dapper.Postgres
{
    /// <summary>
    /// 
    /// </summary>
    public class PostgresConnectionFactory : IDatabaseConnectionFactory
    {
        internal readonly string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        public PostgresConnectionFactory(string connectionString) => _connectionString = connectionString.ThrowIfNull(nameof(connectionString));

        /// <inheritdoc/>
        public async Task<IDbConnection> CreateConnectionAsync() 
        {
            var sqlConnection = new NpgsqlConnection(_connectionString);
            D.DefaultTypeMap.MatchNamesWithUnderscores = true;
            await sqlConnection.OpenAsync();
            return sqlConnection;
        }
    }
}
