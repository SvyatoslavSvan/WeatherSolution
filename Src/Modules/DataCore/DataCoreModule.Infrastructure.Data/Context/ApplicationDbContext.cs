using DataCoreModule.Application.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using DataCoreModule.Core.Models.Entities;

namespace DataCoreModule.Infrastructure.Data.Context;

public class ApplicationDbContext(DbContextOptions opts) : DbContext(opts), IApplicationDbContext
{
    public DbSet<EnvironmentalState> EnvironmentalStates { get; set; }
    public DbSet<City> Cities { get; set; }
}