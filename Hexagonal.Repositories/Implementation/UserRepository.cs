using Dapper;
using Hexagonal.Common.Extensions;
using Hexagonal.Domain.Entities.Users;
using Hexagonal.Session;

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
                                Name, 
                                Email, 
                                Password, 
                                LastSignIn, 
                                Type, 
                                StatusInfo, 
                                ProfileImage, 
                                Status, 
                                CreatedAt, 
                                UpdatedAt,
                                IsCompleted
                            )
                            VALUES (
                                @Id, 
                                @Name, 
                                @Email, 
                                @Password, 
                                @LastSignIn, 
                                @Type, 
                                @StatusInfo, 
                                @ProfileImage, 
                                @Status, 
                                @CreatedAt, 
                                @UpdatedAt,
                                @IsCompleted
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
            var query = "SELECT * FROM Users WHERE Email = @Email";
            var param = new { Email = email };

            var result = await Session.Connection.QueryFirstOrDefaultAsync<User>(query, param);
            return result;
        }

        public async Task Update(User user)
        {
            var query = @"UPDATE Users 
                              SET 
                                Name = @Name,
                                Email = @Email,
                                Password = @Password,
                                LastSignIn = @LastSignIn,
                                Type = @Type,
                                StatusInfo = @StatusInfo,
                                ProfileImage = @ProfileImage,
                                Status = @Status,
                                CreatedAt = @CreatedAt,
                                UpdatedAt = @UpdatedAt,
                                IsCompleted = @IsCompleted
                              WHERE Id = @Id";

            await Session.Connection.ExecuteAsync(query.ToFlat(), user, Session.Transaction);
        }

    }
}
