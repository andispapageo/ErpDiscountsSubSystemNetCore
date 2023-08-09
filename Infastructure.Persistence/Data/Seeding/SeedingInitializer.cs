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

       
    }
}
