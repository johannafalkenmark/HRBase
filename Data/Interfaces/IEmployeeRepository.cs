using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<EmployeeBasicEntity>> GetAllAsync();
    Task<EmployeeBasicEntity?> GetAsync(Expression<Func<EmployeeBasicEntity, bool>> expression);
}
