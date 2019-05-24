using System;

namespace Identity.Dapper.Postgres.Models
{
    internal class UserToken
    {
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
