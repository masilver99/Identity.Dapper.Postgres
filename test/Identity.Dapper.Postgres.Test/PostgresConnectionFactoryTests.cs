using Microsoft.VisualStudio.TestTools.UnitTesting;
using Identity.Dapper.Postgres;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Dapper.Postgres.Tests
{
    [TestClass()]
    public class PostgresConnectionFactoryTests
    {
        [TestMethod()]
        public void Constructor()
        {
            // assign
            var connectionString = "Connection String";

            // act
            var factory = new PostgresConnectionFactory(connectionString);

            // asssert
            Assert.IsNotNull(factory);
            Assert.AreEqual(connectionString, factory._connectionString);
        }

        [TestMethod()]
        public void Constructor_NullConectionString()
        {
            // assign & act & assert
            Assert.ThrowsException<ArgumentNullException>(() => new PostgresConnectionFactory(null));
        }
    }
}