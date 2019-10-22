/*
 * The following code is inspired from https://github.com/aspnet/Identity/blob/master/src/EF/IdentityEntityFrameworkBuilderExtensions.cs
 */

using System;
using Identity.Dapper.Postgres.Stores;
using Identity.Dapper.Postgres.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Identity.Dapper.Postgres.Tables;

namespace Identity.Dapper.Postgres
{
    /// <summary>
    /// Extension methods on <see cref="IdentityBuilder"/> class.
    /// </summary>
    public static class IdentityBuilderExtensions
    {
        /// <summary>
        /// Adds a Dapper implementation of ASP.NET Core Identity stores.
        /// </summary>
        /// <param name="builder">Helper functions for configuring identity services.</param>
        /// <param name="connectionString">The database connection string.</param>
        /// <returns>The <see cref="IdentityBuilder"/> instance this method extends.</returns>
        public static IdentityBuilder AddDapperStores(this IdentityBuilder builder, string connectionString) 
        {
            AddStores(builder.Services, connectionString);
            return builder;
        }

        private static void AddStores(IServiceCollection services,  string connectionString) 
        {
            services.AddTransient(typeof(UsersTable));
            services.AddTransient(typeof(RoleClaimsTable));
            services.AddTransient(typeof(RolesTable));
            services.AddTransient(typeof(UserClaimsTable));
            services.AddTransient(typeof(UserLoginsTable));
            services.AddTransient(typeof(UserRolesTable));
            services.AddTransient(typeof(UserTokensTable));
            services.AddScoped<IDatabaseConnectionFactory>(provider => new PostgresConnectionFactory(connectionString));
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
        }
    }
}
