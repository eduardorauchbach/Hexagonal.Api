using System.Data;

namespace Hexagonal.Session
{
    public interface IDbSession
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }

        void Dispose();
    }
}