using Domain.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Serilog;
using Domain.Core.Entities.Base;

namespace Domain.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IApplicationDbContext Context { get; set; }
        DbSet<T> dbSet { get; set; }
        public Task<IEnumerable<T>> GetCollectionAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        public IQueryable<T> GetQuery(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        public Task<(CrudEn, int)> InsertOrUpdate(T entity);
        public void Insert(T entity);
        public void Update(T entity);
        IMediator mediator { get; set; }
        ILogger logger { get; set; }
    }
}
