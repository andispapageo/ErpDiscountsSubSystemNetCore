using Domain.Core;
using Domain.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infastructure.Data
{
    public partial class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {

        public virtual DbSet<TbCurrency> TbCurrencies { get; set; } = null!;
        public virtual DbSet<TbCustomer> TbCustomers { get; set; } = null!;
        public virtual DbSet<TbCustomerField> TbCustomerFields { get; set; } = null!;
        public virtual DbSet<TbDiscount> TbDiscounts { get; set; } = null!;
        public virtual DbSet<TbDiscountType> TbDiscountTypes { get; set; } = null!;
        public virtual DbSet<TbOrder> TbOrders { get; set; } = null!;
        public virtual DbSet<TbOrderDiscount> TbOrderDiscounts { get; set; } = null!;
        public virtual DbSet<TbSubscription> TbSubscriptions { get; set; } = null!;
        public virtual DbSet<TbView> TbViews { get; set; } = null!;
        public virtual DbSet<TbField> TbFields { get; set; } = null!;
        public virtual DbSet<TbViewType> TbViewTypes { get; set; } = null!;
        public virtual DbSet<TbCustomerFieldsHistory> TbCustomerFieldsHistories { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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

            modelBuilder.Entity<TbCustomerField>(entity =>
            {
                entity.Property(e => e.ViewValue).HasMaxLength(128);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TbCustomerFields)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.View)
                    .WithMany(p => p.TbCustomerFields)
                    .HasForeignKey(d => d.ViewId);
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

            modelBuilder.Entity<TbField>(entity =>
            {
                entity.ToTable("TbField");

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.View)
                    .WithMany(p => p.TbFields)
                    .HasForeignKey(d => d.ViewId);
            });

            modelBuilder.Entity<TbOrder>(entity =>
            {
                entity.ToTable("TbOrder");

                entity.HasOne(d => d.Customer)
                 .WithMany(p => p.TbOrders)
                 .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<TbOrderDiscount>(entity =>
            {
                entity.ToTable("TbOrderDiscount");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TbOrderDiscounts)
                    .HasForeignKey(d => d.OrderId);


                entity.HasOne(d => d.Discount)
                     .WithMany(p => p.TbOrderDiscounts)
                    .HasForeignKey(d => d.DiscountId);
            });

            modelBuilder.Entity<TbSubscription>(entity =>
            {
                entity.Property(e => e.SubscriptionType).HasMaxLength(128);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TbSubscriptions)
                    .HasForeignKey(d => d.OrderId);
            });

            modelBuilder.Entity<TbView>(entity =>
            {
                entity.ToTable("TbView");

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TbViews)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_TbView_TbType_TypeId");
            });

            modelBuilder.Entity<TbViewType>(entity =>
            {
                entity.ToTable("TbViewType");

                entity.Property(e => e.TypeName).HasMaxLength(128);
            });

            modelBuilder.Entity<TbCustomerFieldsHistory>(entity =>
            {
                entity.ToTable("TbCustomerFieldsHistory");

                entity.Property(e => e.NewViewValue).HasMaxLength(208);

                entity.Property(e => e.OldViewValue).HasMaxLength(208);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TbCustomerFieldsHistories)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.View)
                    .WithMany(p => p.TbCustomerFieldsHistories)
                    .HasForeignKey(d => d.ViewId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}