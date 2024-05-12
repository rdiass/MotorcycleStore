using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using MotorcycleStore.Client.API.Models;
using MotorcycleStore.Core.Messages;

namespace MotorcycleStore.Client.API.Data;

public class DeliveryManDbContext : DbContext
{
    public DbSet<DeliveryMan> DeliveryMen { get; set; }

    public DeliveryManDbContext(DbContextOptions<DeliveryManDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<ValidationResult>();

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DeliveryMan>().ToCollection("DeliveryMan");
    }
}
