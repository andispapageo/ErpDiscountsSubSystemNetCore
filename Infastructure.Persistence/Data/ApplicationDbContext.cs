﻿using Domain.Core.Entities;
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
                entity.ToTable("TbDiscountType").HasKey(x => x.Id).HasName("PK_TbDiscountTypes");
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

        public async void Seed()
        {
            Add(new TbDiscountType() { DiscountType = "Percentage" });
            Add(new TbDiscountType() { DiscountType = "Constant" });

            Add(new TbCurrency() { Name = "USD", Symbol = "$" });
            Add(new TbCurrency() { Name = "EUR", Symbol = "€" });
            await SaveChangesAsync();
        }
    }
}