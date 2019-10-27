using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Postgres.Models;

namespace Identity.Dapper.Postgres.Tables
{
    public class UserTokensRepo
    {
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

        public UserTokensRepo(IDatabaseConnectionFactory databaseConnectionFactory) => _databaseConnectionFactory = databaseConnectionFactory;

        public async Task<IEnumerable<UserToken>> GetTokensAsync(Guid userId)
        {
            const string command = "SELECT * " +
                                   "FROM identity_user_tokens " +
                                   "WHERE user_id = @UserId;";

            using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
            {
                return await sqlConnection.QueryAsync<UserToken>(command, new
                {
                    UserId = userId
                });
            }
        }
    }
}
