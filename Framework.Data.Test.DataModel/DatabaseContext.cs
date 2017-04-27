using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Extensions;

namespace Framework.Data.Test.DataModel
{
    public class DatabaseContext : DbContext
    {    

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.Entity<Dealer>()
            .ToTable("dealer").HasKey(d => d.Id);

        }
    }
}
