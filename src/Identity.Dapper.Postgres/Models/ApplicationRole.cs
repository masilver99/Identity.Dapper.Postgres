using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Identity.Dapper.Postgres.Models
{
    /// <summary>
    /// Represents a Role, similar to groups.  Can ve assigned to users
    /// </summary>
    public class ApplicationRole
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationRole()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the Role
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Normalized version of the Name.  Used for searches, etc
        /// </summary>
        public string NormalizedName { get; set; }
        
        /// <summary>
        /// Unsure
        /// </summary>
        public string ConcurrencyStamp { get; set; }
        
        internal List<Claim> Claims { get; set; }
    }
}
