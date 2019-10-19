﻿using System;

namespace Identity.Dapper.Postgres.Models
{
    internal class UserClaim
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
