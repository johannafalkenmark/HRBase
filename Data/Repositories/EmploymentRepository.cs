using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;

public class EmploymentRepository(DataContext context) : BaseRepository<EmploymentEntity>(context), IEmploymentRepository
{
private readonly DataContext _context = context;
}

