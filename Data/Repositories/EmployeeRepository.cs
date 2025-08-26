using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;

public class EmployeeRepository(DataContext context) : BaseRepository<EmployeeBasicEntity>(context), IEmployeeRepository
{

    private readonly DataContext _context = context;

    public override async Task<IEnumerable<EmployeeBasicEntity>> GetAllAsync()
    {
        var entities = await _context.Employees
                   .Include(x => x.Employment)
                   .ToListAsync();
        return entities;
    }

    public override async Task<EmployeeBasicEntity?> GetAsync(Expression<Func<EmployeeBasicEntity, bool>> expression)
    {

        if (expression == null)
            return null;

        return await _context.Employees
              .Include(x => x.Employment)
              .FirstOrDefaultAsync(expression) ?? null!;
    }
}
