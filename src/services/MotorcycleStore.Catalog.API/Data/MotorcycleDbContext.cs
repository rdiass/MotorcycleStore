using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using MotorcycleStore.Catalog.API.Models;
using MotorcycleStore.Core.Messages;

namespace MotorcycleStore.Catalog.API.Data;

public class MotorcycleDbContext : DbContext
{
    public DbSet<Motorcycle> Motorcycles { get; set; }

    public MotorcycleDbContext(DbContextOptions<MotorcycleDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<ValidationResult>();

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Motorcycle>().ToCollection("Motorcycles");
    }
}
