using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public virtual  DbSet<EmployeeBasicEntity> Employees { get; set; } = null!;
    public virtual DbSet<EmploymentEntity> Employments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<EmploymentEntity>().HasData(
            new EmploymentEntity { Id = 1, EmploymentType = "Tillsvidare" },
            new EmploymentEntity { Id = 2, EmploymentType = "Timanställd" },
            new EmploymentEntity { Id = 3, EmploymentType = "Vikariat" },
            new EmploymentEntity { Id = 4, EmploymentType = "Konsult" },
            new EmploymentEntity { Id = 5, EmploymentType = "Avslutad" }

            );
         
    }

}
