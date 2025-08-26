using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DataContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    //CREATE - SPARAR TILL DATABASEN
    //Virtual gör det möjligt att skriva över/göra eventuella justeringar som önskas i de olika repositories 
    public virtual async Task<bool> CreateAsync(TEntity entity)
    {
        if (entity == null)

            return false!;
        try
        {


            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating {nameof(TEntity)} :: {ex.Message}");

            return false!;
        }
    }


    //READ - LÄSER FRÅN DATABASEN
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading {nameof(TEntity)} :: {ex.Message}");
            return null!;

        }
    }



    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {//Skickar ned  entity får tillbaka ett bool värde ovan om hittad eller ej. 

        if (expression == null)
            return null!;

        var entity = await _dbSet.FirstOrDefaultAsync(expression);
        return entity;
    }



    //UPDATE - UPPDATERAR I DATABASEN
    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {

        if (entity == null)
            return false!;
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Error updating {nameof(TEntity)} :: {ex.Message}");

            return false;
        }
    }


    //DELETE
    public virtual async Task<bool> DeleteAsync(TEntity entity)
    {

        if (entity == null)
            return false!;

        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting {nameof(TEntity)} :: {ex.Message}");
            return false;
        }


    }

    //Kontrollerar om finns eller inte, bara hämta om den finns, kolla av det innan. Nedan skickar då bool värde (any)
    public virtual async Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        var result = await _dbSet.AnyAsync(expression);
        return result;
    }
}
