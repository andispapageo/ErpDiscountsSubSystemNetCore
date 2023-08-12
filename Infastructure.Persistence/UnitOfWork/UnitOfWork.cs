using Domain.Core;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces;
using Infastructure.Data;
using MediatR;
using Serilog;

namespace Infastructure.Persistence.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : BaseEntity
    {
        private readonly IApplicationDbContext context;
        private IRepository<T>? repository;
        private bool disposedValue;
        public UnitOfWork(
            IApplicationDbContext context,
            IRepository<T> repository,
            IMediator mediator,
            ILogger logger)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.repository.Context = context;
            repository.dbSet = ((ApplicationDbContext)context).Set<T>();
            repository.mediator = mediator;
            repository.logger = logger;
        }

        public IRepository<T> Repository { get { return repository; } }

        protected async virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (context is ApplicationDbContext)
                        await ((ApplicationDbContext)context).DisposeAsync();

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
