using Dapper;
using Hexagonal.Common.Extensions;
using Hexagonal.Common.Pagging;
using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Filters.Users;
using Hexagonal.Domain.Views.Users;
using Hexagonal.Session;
using System.Text;

namespace Hexagonal.Repositories.Implementation
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDbSession session) : base(session)
        {

        }

        public async Task Create(User user)
        {
            var query = @"INSERT INTO Users (
                                Id, 
                                CreatedAt, 
                                UpdatedAt, 
                                ProfileId, 
                                Name, 
                                Document, 
                                Email, 
                                Phone, 
                                Password,
                                LastSignIn,
                                Status,
                                IsCompleted,
                                IsAdmin
                            )
                            VALUES (
                                @Id, 
                                @CreatedAt, 
                                @UpdatedAt, 
                                @ProfileId, 
                                @Name, 
                                @Document, 
                                @Email, 
                                @Phone, 
                                @Password,
                                @LastSignIn,
                                @Status,
                                @IsCompleted,
                                @IsAdmin
                            );";

            await Session.Connection.ExecuteScalarAsync(query.ToFlat(), user, Session.Transaction);
        }

        public override async Task<User> Get(Guid id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            var param = new { Id = id };

            var result = await Session.Connection.QueryFirstOrDefaultAsync<User>(query, param);
            return result;
        }

        public async Task<User> Get(string email)
        {
            var query = "SELECT * FROM Users WHERE LOWER(Email) = LOWER(@Email)";
            var param = new { Email = email };

            var result = await Session.Connection.QueryFirstOrDefaultAsync<User>(query, param);
            return result;
        }

        public async Task Update(User user)
        {
            var query = @"UPDATE Users 
                              SET 
                                CreatedAt = @CreatedAt,
                                UpdatedAt = @UpdatedAt,
                                ProfileId = @ProfileId,
                                Name = @Name,
                                Document = @Document,
                                Email = @Email,
                                Phone = @Phone,
                                Password = @Password,
                                LastSignIn = @LastSignIn,
                                Status = @Status,
                                IsCompleted = @IsCompleted
                              WHERE Id = @Id";

            await Session.Connection.ExecuteAsync(query.ToFlat(), user, Session.Transaction);
        }

        public async Task<PageResponse<GetAllView>> GetAll(GetAllFilter filter)
        {
            var userQueryBuilder = new StringBuilder(@"
                                                        SELECT 
                                                            u.Id,
                                                            u.Name,
                                                            u.Document,
                                                            u.Email,
                                                            u.Phone,
                                                            u.Password,
                                                            u.ProfileImage,
                                                            u.LastSignIn,
                                                            u.Status,
                                                            u.IsCompleted,
                                                            u.ProfileId,
                                                            p.Name as ProfileName,
                                                            p.Name as ProfileName,
                                                            u.IsAdmin,
                                                            CASE WHEN pa1.Area IS NOT NULL THEN TRUE ELSE FALSE END AS HasSchoolAccess,
                                                            CASE WHEN pa2.Area IS NOT NULL THEN TRUE ELSE FALSE END AS HasStockAccess,
                                                            CASE WHEN pa3.Area IS NOT NULL THEN TRUE ELSE FALSE END AS HasOrdersAccess
                                                        FROM Users u
                                                        LEFT JOIN Profiles p ON u.ProfileId = p.Id
                                                        LEFT JOIN ProfileAreas pa1 ON p.Id = pa1.ProfileId AND pa1.Area = 1 AND (pa1.CanAdd OR pa1.CanUpdate OR pa1.CanDelete)
                                                        LEFT JOIN ProfileAreas pa2 ON p.Id = pa2.ProfileId AND pa2.Area = 2 AND (pa2.CanAdd OR pa2.CanUpdate OR pa2.CanDelete)
                                                        LEFT JOIN ProfileAreas pa3 ON p.Id = pa3.ProfileId AND pa3.Area = 3 AND (pa3.CanAdd OR pa3.CanUpdate OR pa3.CanDelete)
                                                        WHERE u.IsAdmin = @IsAdmin
                                                    ");

            var countQueryBuilder = new StringBuilder(@"
                                                        SELECT COUNT(*)
                                                        FROM Users u
                                                        WHERE u.IsAdmin = @IsAdmin
                                                    ");

            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            // Adding conditional parameters
            if (!string.IsNullOrEmpty(filter.Name))
            {
                conditions.Add("LOWER(u.Name) LIKE LOWER(@Name)");
                parameters.Add("Name", $"%{filter.Name}%");
            }

            if (conditions.Any())
            {
                var conditionString = string.Join(" AND ", conditions);
                userQueryBuilder.Append(" AND " + conditionString);
                countQueryBuilder.Append(" AND " + conditionString);
            }

            userQueryBuilder.Append(@"
                                        ORDER BY @OrderBy
                                        LIMIT @PageSize OFFSET @Offset;
                                    ");

            parameters.Add("IsAdmin", filter.IsAdmin);
            parameters.Add("OrderBy", $"{filter.OrderBy.Column} {filter.OrderBy.Direction}");
            parameters.Add("PageSize", filter.PageSize);
            parameters.Add("Offset", filter.Offset);

            var users = await Session.Connection.QueryAsync<GetAllView>(
                userQueryBuilder.ToString(),
                parameters,
                Session.Transaction
            );

            var totalItems = await Session.Connection.ExecuteScalarAsync<int>(
                countQueryBuilder.ToString(),
                parameters,
                Session.Transaction
            );

            return new PageResponse<GetAllView>
            {
                Items = users.ToList(),
                CurrentPage = filter.CurrentPage,
                PageSize = filter.PageSize,
                TotalItems = totalItems
            };
        }
    }
}
