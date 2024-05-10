using Dapper;
using Hexagonal.Common.Extensions;
using Hexagonal.Domain.Domain.Entities.Verifications;
using Hexagonal.Session;

namespace Hexagonal.Repositories.Implementation
{
    internal class VerificationRepository : Repository<Verification>, IVerificationRepository
    {
        public VerificationRepository(IDbSession session) : base(session)
        {
        }

        public async Task Create(Verification verification)
        {
            var query = @"INSERT INTO Verifications (
                                Id,
                                CreatedAt,
                                UpdatedAt,
                                Origin,
                                Value,
                                ExpiresAt,
                                Type
                            ) VALUES (
                                @Id,
                                @CreatedAt,
                                @UpdatedAt,
                                @Origin,
                                @Value,
                                @ExpiresAt,
                                @Type
                            );";

            await Session.Connection.ExecuteAsync(query.ToFlat(), verification, Session.Transaction);
        }

        public override async Task<Verification> Get(Guid id)
        {
            var query = "SELECT * FROM Verifications WHERE Id = @Id";
            var param = new { Id = id };

            var result = await Session.Connection.QueryFirstOrDefaultAsync<Verification>(query, param);
            return result;
        }
    }
}
