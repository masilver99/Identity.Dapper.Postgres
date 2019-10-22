using Identity.Dapper.Postgres.Tables;
using S = Identity.Dapper.Postgres.Stores;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Identity.Dapper.Postgres.Test.UserStore
{
    [TestClass]
    public class UserStore
    {
        [TestMethod]
        public void Constructor()
        {
            // assign
            var usersTable = new Mock<UsersTable>(null).Object;
            var userRolesTable = new Mock<UserRolesTable>(null).Object;
            var rolesTable = new Mock<RolesTable>(null).Object;
            var userClaimsTable = new Mock<UserClaimsTable>(null).Object;
            var userLoginsTable = new Mock<UserLoginsTable>(null).Object;
            var userTokensTable = new Mock<UserTokensTable>(null).Object;

            // act
            var userStore = new S.UserStore(usersTable, userRolesTable, rolesTable, userClaimsTable, userLoginsTable, userTokensTable);

            // assert
            Assert.AreEqual(usersTable, userStore._usersTable);
            Assert.AreEqual(userRolesTable, userStore._userRolesTable);
            Assert.AreEqual(rolesTable, userStore._rolesTable);
            Assert.AreEqual(userClaimsTable, userStore._userClaimsTable);
            Assert.AreEqual(userLoginsTable, userStore._userLoginsTable);
            Assert.AreEqual(userTokensTable, userStore._userTokensTable);
        }
    }
}