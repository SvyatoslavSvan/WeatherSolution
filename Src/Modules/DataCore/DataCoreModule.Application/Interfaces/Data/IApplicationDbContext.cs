using DataCoreModule.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataCoreModule.Application.Interfaces.Data;

public interface IApplicationDbContext
{
    public DbSet<EnvironmentalState> EnvironmentalStates { get; set; }
    public DbSet<City> Cities { get; set; }
}