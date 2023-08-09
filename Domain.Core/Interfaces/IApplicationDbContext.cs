using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Core
{
    public interface IApplicationDbContext
    {
        public DbSet<TbCurrency> TbCurrencies { get; set; }
        public DbSet<TbCustomer> TbCustomers { get; set; }
        public DbSet<TbDiscount> TbDiscounts { get; set; }
        public DbSet<TbDiscountType> TbDiscountTypes { get; set; }
        public DbSet<TbOrder> TbOrders { get; set; }
        public DbSet<TbOrderDiscount> TbOrderDiscounts { get; set; }
        public DbSet<TbSubscription> TbSubscriptions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
