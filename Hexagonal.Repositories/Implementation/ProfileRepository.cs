using Dapper;
using Hexagonal.Common.Extensions;
using Hexagonal.Domain.Entities.Profiles;
using Hexagonal.Domain.Entities.Users;
using Hexagonal.Session;

namespace Hexagonal.Repositories.Implementation
{
    internal class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(IDbSession session) : base(session)
        {
        }

        public async Task Create(Profile profile)
        {
            var query = @"INSERT INTO Profiles (
                                Id, 
                                CreatedAt, 
                                UpdatedAt, 
                                Name
                            )
                            VALUES (
                                @Id, 
                                @CreatedAt, 
                                @UpdatedAt, 
                                @Name
                            );";

            await Session.Connection.ExecuteScalarAsync(query.ToFlat(), profile, Session.Transaction);

            foreach (var area in profile.ProfileAreas)
            {
                await CreateProfileArea(area);
            }
        }

        public async Task Delete(Guid id)
        {
            var query = "DELETE FROM Profiles WHERE Id = @Id";
            var param = new { Id = id };

            await Session.Connection.ExecuteAsync(query, param);
        }

        public override async Task<Profile> Get(Guid id)
        {
            var query = @"
                            SELECT * FROM Profiles WHERE Id = @Id;
                            SELECT * FROM ProfileAreas WHERE ProfileId = @Id;
                        ";

            var param = new { Id = id };

            using var multi = await Session.Connection.QueryMultipleAsync(query, param, Session.Transaction);

            var profile = await multi.ReadFirstOrDefaultAsync<Profile>();
            if (profile != null)
            {
                profile.ProfileAreas = (await multi.ReadAsync<ProfileArea>()).ToList();
            }

            return profile;
        }

        public async Task<Profile> Get(string name)
        {
            var query = @"
                            SELECT * FROM Profiles WHERE LOWER(Name) = LOWER(@Name);
                        ";

            var param = new { Name = name };

            return await Session.Connection.QueryFirstOrDefaultAsync<Profile>(query, param, Session.Transaction);
        }

        public async Task<IEnumerable<Profile>> GetAll()
        {
            var query = @" SELECT * FROM Profiles;";
            var profiles = await Session.Connection.QueryAsync<Profile>(query, null, Session.Transaction);

            var queryArea = @" SELECT * FROM ProfileAreas;";
            var profileArea = await Session.Connection.QueryAsync<ProfileArea>(queryArea, null, Session.Transaction);

            foreach (var p in profiles)
            {
                p.ProfileAreas = profileArea.Where(x => x.ProfileId == p.Id).ToList();
            }

            return profiles;
        }

        public async Task Update(Profile profile)
        {
            var query = @"UPDATE Profiles 
                              SET 
                                CreatedAt = @CreatedAt,
                                UpdatedAt = @UpdatedAt,
                                Name = @Name
                              WHERE Id = @Id";

            await Session.Connection.ExecuteAsync(query.ToFlat(), profile, Session.Transaction);

            var deleteQuery = "DELETE FROM ProfileAreas WHERE ProfileId = @ProfileId";
            await Session.Connection.ExecuteAsync(deleteQuery, new { ProfileId = profile.Id }, Session.Transaction);

            foreach (var area in profile.ProfileAreas)
            {
                area.ProfileId = profile.Id.Value;
                await CreateProfileArea(area);
            }
        }

        private async Task CreateProfileArea(ProfileArea area)
        {
            var query = @"INSERT INTO ProfileAreas (
                                ProfileId, 
                                Area, 
                                CanAdd, 
                                CanUpdate, 
                                CanDelete
                            )
                            VALUES (
                                @ProfileId, 
                                @Area, 
                                @CanAdd, 
                                @CanUpdate, 
                                @CanDelete
                            );";

            await Session.Connection.ExecuteScalarAsync(query.ToFlat(), area, Session.Transaction);
        }
    }
}
