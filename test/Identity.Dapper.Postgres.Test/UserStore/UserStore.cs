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
            var usersRepo = new Mock<UsersRepo>(null).Object;
            var userRolesRepo = new Mock<UserRolesRepo>(null).Object;
            var rolesRepo = new Mock<RolesRepo>(null).Object;
            var userClaimsRepo = new Mock<UserClaimsRepo>(null).Object;
            var userLoginsRepo = new Mock<UserLoginsRepo>(null).Object;
            var userTokensRepo = new Mock<UserTokensRepo>(null).Object;

            // act
            using var userStore = new S.UserStore(usersRepo, userRolesRepo, rolesRepo, userClaimsRepo, userLoginsRepo, userTokensRepo);
            
            // assert
            Assert.AreEqual(usersRepo, userStore._usersRepo);
            Assert.AreEqual(userRolesRepo, userStore._userRolesRepo);
            Assert.AreEqual(rolesRepo, userStore._rolesRepo);
            Assert.AreEqual(userClaimsRepo, userStore._userClaimsRepo);
            Assert.AreEqual(userLoginsRepo, userStore._userLoginsRepo);
            Assert.AreEqual(userTokensRepo, userStore._userTokensRepo);
        }

        [TestMethod]
        public void Constructor_NullUsersRepo()
        {
            // assign
            var factory = new Mock<IDatabaseConnectionFactory>().Object;
            UsersRepo usersRepo = null;
            var userRolesRepo = new Mock<UserRolesRepo>(factory).Object;
            var rolesRepo = new Mock<RolesRepo>(factory).Object;
            var userClaimsRepo = new Mock<UserClaimsRepo>(factory).Object;
            var userLoginsRepo = new Mock<UserLoginsRepo>(factory).Object;
            var userTokensRepo = new Mock<UserTokensRepo>(factory).Object;

            // act & assert
            Assert.ThrowsException<ArgumentNullException>(() => new S.UserStore(usersRepo, userRolesRepo, rolesRepo, userClaimsRepo, userLoginsRepo, userTokensRepo));
        }

        [TestMethod]
        public void Constructor_NullUserRolesRepo()
        {
            // assign
            var factory = new Mock<IDatabaseConnectionFactory>().Object;
            var usersRepo = new Mock<UsersRepo>(factory).Object;
            UserRolesRepo userRolesRepo = null;
            var rolesRepo = new Mock<RolesRepo>(factory).Object;
            var userClaimsRepo = new Mock<UserClaimsRepo>(factory).Object;
            var userLoginsRepo = new Mock<UserLoginsRepo>(factory).Object;
            var userTokensRepo = new Mock<UserTokensRepo>(factory).Object;

            // act & assert
            Assert.ThrowsException<ArgumentNullException>(() => new S.UserStore(usersRepo, userRolesRepo, rolesRepo, userClaimsRepo, userLoginsRepo, userTokensRepo));
        }

        [TestMethod]
        public void Constructor_NullRolesRepo()
        {
            // assign
            var factory = new Mock<IDatabaseConnectionFactory>().Object;
            var usersRepo = new Mock<UsersRepo>(factory).Object;
            var userRolesRepo = new Mock<UserRolesRepo>(factory).Object;
            RolesRepo rolesRepo = null;
            var userClaimsRepo = new Mock<UserClaimsRepo>(factory).Object;
            var userLoginsRepo = new Mock<UserLoginsRepo>(factory).Object;
            var userTokensRepo = new Mock<UserTokensRepo>(factory).Object;

            // act & assert
            Assert.ThrowsException<ArgumentNullException>(() => new S.UserStore(usersRepo, userRolesRepo, rolesRepo, userClaimsRepo, userLoginsRepo, userTokensRepo));
        }

        [TestMethod]
        public void Constructor_NullUserClaimsRepo()
        {
            // assign
            var factory = new Mock<IDatabaseConnectionFactory>().Object;
            var usersRepo = new Mock<UsersRepo>(factory).Object;
            var userRolesRepo = new Mock<UserRolesRepo>(factory).Object;
            var rolesRepo = new Mock<RolesRepo>(factory).Object;
            UserClaimsRepo userClaimsRepo = null;
            var userLoginsRepo = new Mock<UserLoginsRepo>(factory).Object;
            var userTokensRepo = new Mock<UserTokensRepo>(factory).Object;

            // act & assert
            Assert.ThrowsException<ArgumentNullException>(() => new S.UserStore(usersRepo, userRolesRepo, rolesRepo, userClaimsRepo, userLoginsRepo, userTokensRepo));
        }

        [TestMethod]
        public void Constructor_NullUserLoginsRepo()
        {
            // assign
            var factory = new Mock<IDatabaseConnectionFactory>().Object;
            var usersRepo = new Mock<UsersRepo>(factory).Object;
            var userRolesRepo = new Mock<UserRolesRepo>(factory).Object;
            var rolesRepo = new Mock<RolesRepo>(factory).Object;
            var userClaimsRepo = new Mock<UserClaimsRepo>(factory).Object;
            UserLoginsRepo userLoginsRepo = null;
            var userTokensRepo = new Mock<UserTokensRepo>(factory).Object;

            // act & assert
            Assert.ThrowsException<ArgumentNullException>(() => new S.UserStore(usersRepo, userRolesRepo, rolesRepo, userClaimsRepo, userLoginsRepo, userTokensRepo));
        }

        [TestMethod]
        public void Constructor_NullUserTokensRepo()
        {
            // assign
            var factory = new Mock<IDatabaseConnectionFactory>().Object;
            var usersRepo = new Mock<UsersRepo>(factory).Object;
            var userRolesRepo = new Mock<UserRolesRepo>(factory).Object;
            var rolesRepo = new Mock<RolesRepo>(factory).Object;
            var userClaimsRepo = new Mock<UserClaimsRepo>(factory).Object;
            var userLoginsRepo = new Mock<UserLoginsRepo>(factory).Object;
            UserTokensRepo userTokensRepo = null;

            // act & assert
            Assert.ThrowsException<ArgumentNullException>(() => new S.UserStore(usersRepo, userRolesRepo, rolesRepo, userClaimsRepo, userLoginsRepo, userTokensRepo));
        }
    }
}