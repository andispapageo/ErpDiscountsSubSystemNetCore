using Domain.Core;
using Domain.Core.Interfaces;
using Infastructure.Data;

namespace Infastructure.Persistence.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly IApplicationDbContext context;
        private IRepository<T>? repository;
        private bool disposedValue;
        public UnitOfWork(IApplicationDbContext context, IRepository<T> repository)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.repository.Context = context;
            repository.dbSet = ((ApplicationDbContext)context).Set<T>();
        }

        public IRepository<T> Repository { get { return repository; } }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (context is ApplicationDbContext)
                        ((ApplicationDbContext)context).Dispose();
                    repository = null;
                }
            }
        }

        ~UnitOfWork() => Dispose(false);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
