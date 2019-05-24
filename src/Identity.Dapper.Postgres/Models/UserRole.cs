using System;

namespace Identity.Dapper.Postgres.Models
{
    internal class UserRole
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
