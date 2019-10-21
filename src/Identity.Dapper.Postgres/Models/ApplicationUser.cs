using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Identity.Dapper.Postgres.Models
{
    /// <summary>
    /// Represents a user for authentication
    /// </summary>
    public class ApplicationUser
    {
        /// <summary>
        /// Constructor.  Sets primary key.
        /// </summary>
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Username used to login and/or identify the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Consistent version of UserName to allow for easier searching
        /// </summary>
        public string NormalizedUserName { get; set; }

        /// <summary>
        /// Email address associated with user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Consistent, normalized version of Email address sto allow for easier searching
        /// </summary>
        public string NormalizedEmail { get; set; }

        /// <summary>
        /// Has the email address been confirmed?
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Hashed password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Unsure
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// Unsure
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Has phone number been confirmed?
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Is two factor authentication enabled?
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// If populated, user is locked out until datetime
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Is lockout enabled?  I.e. after x number of bad tries, user will be locked out.
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Number of failed attempts at logging in
        /// </summary>
        public int AccessFailedCount { get; set; }

        internal List<Claim> Claims { get; set; }

        internal List<UserRole> Roles { get; set; }

        internal List<UserLoginInfo> Logins { get; set; }

        internal List<UserToken> Tokens { get; set; }
    }
}
