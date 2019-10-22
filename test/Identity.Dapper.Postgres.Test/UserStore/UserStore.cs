using Identity.Dapper.Postgres.Tables;
using S = Identity.Dapper.Postgres.Stores;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Identity.Dapper.Postgres.Test.UserStore
{
    public class UserStore
    {
        [Fact]
        public void Constructor()
        {
            // assign
            var usersTable = Mock.Of<UsersTable>();
            var userRolesTable = Mock.Of<UserRolesTable>();
            var rolesTable = Mock.Of<RolesTable>();
            var userClaimsTable = Mock.Of<UserClaimsTable>();
            var userLoginsTable = Mock.Of<UserLoginsTable>();
            var userTokensTable = Mock.Of<UserTokensTable>();

            // act
            var userStore = new S.UserStore(usersTable, userRolesTable, rolesTable, userClaimsTable, userLoginsTable, userTokensTable);

            // assert
            Assert.Equal(usersTable, userStore._usersTable);
            Assert.Equal(userRolesTable, userStore._userRolesTable);
            Assert.Equal(rolesTable, userStore._rolesTable);
            Assert.Equal(userClaimsTable, userStore._userClaimsTable);
            Assert.Equal(userLoginsTable, userStore._userLoginsTable);
            Assert.Equal(userTokensTable, userStore._userTokensTable);
        }
    }
}