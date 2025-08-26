﻿using Data.Entities;
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
    public virtual  DbSet<EmployeeBasicEntity> EmployeesBasicInfo { get; set; } = null!;
    public virtual DbSet<EmploymentEntity> Employments { get; set; } = null!;
}
