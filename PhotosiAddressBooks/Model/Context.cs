using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace PhotosiAddressBooks.Model;

[ExcludeFromCodeCoverage]
public class Context : DbContext
{
    public virtual DbSet<AddressBook> AddressBooks { get; set; }

    public Context()
    {
    }

    public Context(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressBook>()
            .HasIndex(x => new {x.AddressName, x.AddressNumber, x.Cap, x.CityName, x.CountryName})
            .IsUnique()
            ;
    }
}