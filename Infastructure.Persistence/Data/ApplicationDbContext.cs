using Domain.Core.Entities;
using Domain.Core.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {


        public virtual DbSet<TbCurrency> TbCurrencies { get; set; } = null!;
        public virtual DbSet<TbCustomer> TbCustomers { get; set; } = null!;
        public virtual DbSet<TbDiscount> TbDiscounts { get; set; } = null!;
        public virtual DbSet<TbDiscountType> TbDiscountTypes { get; set; } = null!;
        public virtual DbSet<TbOrder> TbOrders { get; set; } = null!;
        public virtual DbSet<TbOrderDiscount> TbOrderDiscounts { get; set; } = null!;
        public virtual DbSet<TbSubscription> TbSubscriptions { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TbCurrency>(entity =>
            {
                entity.ToTable("TbCurrency").HasKey(x => x.Id).HasName("PK_TbCurrency");

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.Symbol).HasMaxLength(10);
            });

            modelBuilder.Entity<TbCustomer>(entity =>
            {
                entity.ToTable("TbCustomer").HasKey(x => x.Id).HasName("PK_TbCustomer");

                entity.Property(e => e.Address).HasMaxLength(128);

                entity.Property(e => e.LastName).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);
            });

            modelBuilder.Entity<TbDiscount>(entity =>
            {
                entity.ToTable("TbDiscount").HasKey(x => x.Id).HasName("PK_TbDiscount");

                entity.Property(e => e.DiscountName).HasMaxLength(128);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.TbDiscounts)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_TbDiscounts_TbCurrency_CurrencyId");

                entity.HasOne(d => d.DiscountType)
                    .WithMany(p => p.TbDiscounts)
                    .HasForeignKey(d => d.DiscountTypeId);
            });

            modelBuilder.Entity<TbDiscountType>(entity =>
            {
                entity.ToTable("TbDiscountTypes").HasKey(x => x.Id).HasName("PK_TbDiscountTypes");
                entity.Property(e => e.DiscountType).HasMaxLength(128);
            });

            modelBuilder.Entity<TbOrder>(entity =>
            {
                entity.ToTable("TbOrder");
            });

            modelBuilder.Entity<TbOrderDiscount>(entity =>
            {
                entity.ToTable("TbOrderDiscount");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TbOrderDiscounts)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TbOrderDiscounts)
                    .HasForeignKey(d => d.OrderId);
            });

            modelBuilder.Entity<TbSubscription>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.Symbol).HasMaxLength(10);
            });
        }

        public async Task<int> Seed()
        {
            int setup = -1;
            if (TbDiscountTypes.Count() == 0)
            {
                await TbDiscountTypes.AddAsync(new TbDiscountType() { DiscountType = DiscountTypeEn.Percentage.ToString() });
                await TbDiscountTypes.AddAsync(new TbDiscountType() { DiscountType = DiscountTypeEn.Coupon.ToString() });
                await SaveChangesAsync();
            }

            if (TbCurrencies.Count() == 0)
            {
                await TbCurrencies.AddAsync(new TbCurrency() { Name = "USD", Symbol = "$" });
                await TbCurrencies.AddAsync(new TbCurrency() { Name = "EUR", Symbol = "€" });
                await SaveChangesAsync();
            }

            if (TbDiscounts.Count() == 0)
            {
                var currency = await TbCurrencies?.FirstOrDefaultAsync(x => x.Name == CurrencyEn.EUR.ToString()) ?? default;
                var percentageType = await TbDiscountTypes.FirstOrDefaultAsync(x => x.DiscountType == DiscountTypeEn.Percentage.ToString()) ?? default;
                var couponType = await TbDiscountTypes.FirstOrDefaultAsync(x => x.DiscountType == DiscountTypeEn.Coupon.ToString()) ?? default;

                if (currency == default || percentageType == default || couponType == default) return setup;

                await TbDiscounts.AddAsync(new TbDiscount()
                {
                    DiscountName = "PriceLists",
                    CurrencyId = currency.Id,
                    DiscountTypeId = percentageType?.Id ?? 1,

                    Price = 5,
                });

                await TbDiscounts.AddAsync(new TbDiscount()
                {
                    DiscountName = "Promotions",
                    CurrencyId = currency.Id,
                    DiscountTypeId = percentageType.Id,
                    Price = 10,
                });

                await TbDiscounts.AddAsync(new TbDiscount()
                {
                    DiscountName = "Coupons",
                    CurrencyId = currency.Id,
                    DiscountTypeId = couponType.Id,
                    Price = 10,
                });
            }

            if (TbCustomers.Count() == 0)
            {
                await TbCustomers.AddAsync(new TbCustomer() { Name = "MockUserName", Address = "MockAddress", LastName = "MockLastName" });
            }

            if (ChangeTracker.HasChanges())
            {
                setup = 1;
                await SaveChangesAsync();
            }
            return setup;
        }
    }
}