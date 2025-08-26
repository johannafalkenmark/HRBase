using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> CreateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> UpdateAsync(TEntity entity);
}
