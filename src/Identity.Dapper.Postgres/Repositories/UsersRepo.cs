using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Identity.Dapper.Postgres.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Dapper.Postgres.Tables
{
    public class UsersRepo
    {
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

        public UsersRepo(IDatabaseConnectionFactory databaseConnectionFactory) => _databaseConnectionFactory = databaseConnectionFactory;

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {


            const string command = "INSERT INTO identity_users " +
                                   " (id, username, normalized_username, email, normalized_email, email_confirmed, password_hash, security_stamp, concurrency_stamp, " +
                                   "phone_number, phone_number_confirmed, two_factor_enabled, lockout_end, lockout_enabled, access_failed_count) " +
                                   "VALUES (@Id, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed, @PasswordHash, @SecurityStamp, @ConcurrencyStamp, " +
                                           "@PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEnd, @LockoutEnabled, @AccessFailedCount);";

            int rowsInserted;

            using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
            {
                rowsInserted = await sqlConnection.ExecuteAsync(command, new
                {
                    user.Id,
                    user.UserName,
                    user.NormalizedUserName,
                    user.Email,
                    user.NormalizedEmail,
                    user.EmailConfirmed,
                    user.PasswordHash,
                    user.SecurityStamp,
                    user.ConcurrencyStamp,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    user.TwoFactorEnabled,
                    user.LockoutEnd,
                    user.LockoutEnabled,
                    user.AccessFailedCount
                });
            }

            return rowsInserted == 1 ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
            {
                Code = nameof(CreateAsync),
                Description = $"User with email {user.Email} could not be inserted."
            });
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            const string command = "delete " +
                                   "FROM identity_users " +
                                   "WHERE id = @Id;";

            int rowsDeleted;

            using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
            {
                rowsDeleted = await sqlConnection.ExecuteAsync(command, new
                {
                    user.Id
                });
            }

            return rowsDeleted == 1 ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
            {
                Code = nameof(DeleteAsync),
                Description = $"User with email {user.Email} could not be deleted."
            });
        }

        public async Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            const string command = "SELECT * " +
                                   "FROM identity_users " +
                                   "WHERE id = @userId;";

            using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<ApplicationUser>(command, new
                {
                    userId
                });
            }
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName)
        {
            const string command = "SELECT * " +
                                   "FROM identity_users " +
                                   "WHERE normalized_username = @NormalizedUserName;";

            using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<ApplicationUser>(command, new
                {
                    NormalizedUserName = normalizedUserName
                });
            }
        }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail)
        {
            const string command = "SELECT * " +
                                   "FROM identity_users " +
                                   "WHERE normalized_email = @NormalizedEmail;";

            using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<ApplicationUser>(command, new
                {
                    NormalizedEmail = normalizedEmail
                });
            }
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            //NOTE: Removing the UOW pattern, so comments below no longer apply
            // The implementation here might look a little strange, however there is a reason for this.
            // ASP.NET Core Identity stores follow a UOW (Unit of Work) pattern which practically means that when an operation is called it does not necessarily writes to the database.
            // It tracks the changes made and finally commits to the database. UserStore methods just manipulate the user and only CreateAsync, UpdateAsync and DeleteAsync of IUserStore<>
            // write to the database. This makes sense because this way we avoid connection to the database all the time and also we can commit all changes at once by using a transaction.
            const string updateUserCommand =
                "UPDATE identity_users " +
                "SET username = @UserName, normalized_username = @NormalizedUserName, email = @Email, normalized_email = @NormalizedEmail, email_confirmed = @EmailConfirmed, " +
                "password_hash = @PasswordHash, security_stamp = @SecurityStamp, concurrency_stamp = @ConcurrencyStamp, phone_number = @PhoneNumber, " +
                "phone_number_confirmed = @PhoneNumberConfirmed, two_factor_enabled = @TwoFactorEnabled, lockout_end = @LockoutEnd, lockout_enabled = @LockoutEnabled, " +
                "access_failed_count = @AccessFailedCount " +
                "WHERE id = @Id;";
            try
            {
                using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
                {
                    await sqlConnection.ExecuteAsync(updateUserCommand, new
                    {
                        user.UserName,
                        user.NormalizedUserName,
                        user.Email,
                        user.NormalizedEmail,
                        user.EmailConfirmed,
                        user.PasswordHash,
                        user.SecurityStamp,
                        user.ConcurrencyStamp,
                        user.PhoneNumber,
                        user.PhoneNumberConfirmed,
                        user.TwoFactorEnabled,
                        user.LockoutEnd,
                        user.LockoutEnabled,
                        user.AccessFailedCount,
                        user.Id
                    });
                }
            }
            catch
            {
                //TODO: Not sure we should be eating exceptions
                return IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(UpdateAsync),
                    Description =
                        $"User with email {user.Email} could not be updated. Operation was rolled back."
                });
            }

            return IdentityResult.Success;
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            const string command = "SELECT * " +
                                   "FROM identity_users AS u " +
                                   "INNER JOIN identity_user_roles AS ur ON u.id = ur.user_id " +
                                   "INNER JOIN identity_roles AS r ON ur.role_id = r.id " +
                                   "WHERE r.name = @RoleName;";

            using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
            {
                return (await sqlConnection.QueryAsync<ApplicationUser>(command, new
                {
                    RoleName = roleName
                })).ToList();
            }
        }

        public async Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim)
        {
            const string command = "SELECT * " +
                                   "FROM identity_users AS u " +
                                   "INNER JOIN identity_user_claims AS uc ON u.id = uc.user_id " +
                                   "WHERE uc.claim_type = @ClaimType AND uc.claim_value = @ClaimValue;";

            using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
            {
                return (await sqlConnection.QueryAsync<ApplicationUser>(command, new
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                })).ToList();
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            const string command = "SELECT * " +
                                   "FROM identity_users;";

            using (var sqlConnection = await _databaseConnectionFactory.CreateConnectionAsync())
            {
                return await sqlConnection.QueryAsync<ApplicationUser>(command);
            }
        }
    }
}
