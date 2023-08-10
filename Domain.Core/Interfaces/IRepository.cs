using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IApplicationDbContext Context { get; set; }
        DbSet<T> dbSet { get; set; }
        public IEnumerable<T> GetCollection(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        public Task<IEnumerable<T>> GetCollectionAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        public IQueryable<T> GetQuery(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        public void Insert(T entity);
        public void Update(T entity);
    }
}
