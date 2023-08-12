using Domain.Core.Entities.Base;

namespace Domain.Core.Interfaces
{
    public interface IUnitOfWork<T> : IDisposable where T : BaseEntity
    {
        IRepository<T> Repository { get; }
    }
}
