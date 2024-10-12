using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Price, m =>
            {
                m.Property(p => p.Price).HasColumnName("PriceAmount");
                m.Property(p => p.Currency).HasColumnName("CurrencyCode");
            });

        
    }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

}
