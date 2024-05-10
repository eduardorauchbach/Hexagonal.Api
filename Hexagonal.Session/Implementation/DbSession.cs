using Microsoft.Extensions.Options;
using Npgsql;
using Hexagonal.Common.Configurations;
using System.Data;

namespace Hexagonal.Session.Implementation
{
    internal class DbSession : IDisposable, IDbSession
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        private readonly AppSettings _appSettings;

        public DbSession(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;

            Connection = new NpgsqlConnection(_appSettings.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            Connection?.Dispose();
        }
    }
}
