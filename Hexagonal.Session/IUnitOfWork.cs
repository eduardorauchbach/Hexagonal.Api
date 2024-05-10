namespace Hexagonal.Session
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Dispose();
        void Rollback();
    }
}