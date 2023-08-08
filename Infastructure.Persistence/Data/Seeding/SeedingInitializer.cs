using Domain.Core.Entities;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Persistence.Data.Seeding
{
    internal class SeedingInitializer
    {
        private readonly ModelBuilder modelBuilder;
        private ApplicationDbContext applicationDbContext;

        public SeedingInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public SeedingInitializer(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async void Seed()
        {
            modelBuilder.Entity<TbDiscountType>().HasData(
               new TbDiscountType() { Id = 1, DiscountType = "Percentage" },
               new TbDiscountType() { Id = 2, DiscountType = "Constant" });

            modelBuilder.Entity<TbCurrency>().HasData(
               new TbCurrency() { Id = 1, Name = "USD", Symbol = "$" },
               new TbCurrency() { Id = 2, Name = "EUR", Symbol = "€" });

            modelBuilder.Entity<TbDiscount>().HasData(
                 new TbDiscount() { Id = 1, DiscountName = "Price Lists", Price = 5, PriorityOrderId = 1, DiscountTypeId = 1 },
                 new TbDiscount() { Id = 2, DiscountName = "Promotion", Price = 10, PriorityOrderId = 1, DiscountTypeId = 1 },
                 new TbDiscount() { Id = 3, DiscountName = "Coupons", Price = 10, PriorityOrderId = 1, DiscountTypeId = 2 }
            );
        }

       
    }
}
