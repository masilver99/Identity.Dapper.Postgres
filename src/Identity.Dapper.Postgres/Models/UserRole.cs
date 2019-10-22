using System;

namespace Identity.Dapper.Postgres.Models
{
    public class UserRole
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
