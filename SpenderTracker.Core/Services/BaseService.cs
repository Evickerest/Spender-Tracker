using Microsoft.EntityFrameworkCore;
using SpenderTracker.Core.Interfaces;
using SpenderTracker.Data.Context;
using SpenderTracker.Data.Interface;

namespace SpenderTracker.Core.Services;

public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
    where TEntity : class, IEntity<TDto>, new()
    where TDto : class, IDto
{
    protected readonly ApplicationContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseService(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>(); 
    }

    public async virtual Task<TDto?> GetById(int id, CancellationToken ct)
    {
        TEntity? entity = await _dbSet.FindAsync([id], ct);
        if (entity == null) return null;
        return entity.ToDto();
    }

    public async virtual Task<List<TDto>> GetAll(CancellationToken ct)
    {
        var dtos = await _dbSet.AsNoTracking().
            Select(e => e.ToDto()).
            ToListAsync(ct);
        return dtos;
    }

    public async virtual Task<TDto?> Insert(TDto dto)
    {
        TEntity entity = new();
        _dbSet.Entry(entity).CurrentValues.SetValues(dto); 
        _dbSet.Add(entity);

        try
        {
            await _dbContext.SaveChangesAsync();
            return entity.ToDto();
        } catch(DbUpdateException)
        {
            return null; 
        } 
    }

    public virtual async Task<bool> Update(TDto dto)
    {
        TEntity? entity = await _dbSet.FindAsync([dto.Id]);
        if (entity == null) return false;

        _dbSet.Entry(entity).CurrentValues.SetValues(dto);
        if (!_dbContext.ChangeTracker.HasChanges()) return true; 

        try
        {
            await _dbContext.SaveChangesAsync();
            return true; 
        } catch (DbUpdateException)
        {
            return false; 
        } 
    }

    public virtual async Task<bool> Delete(int id)
    { 
        try
        {
            await _dbSet.Where(e => e.Id == id).ExecuteDeleteAsync();
            return true;
        } catch (DbUpdateException)
        {
            return false; 
        } 
    } 

    public virtual async Task<bool> DoesExist(int id, CancellationToken ct)
    {
        bool doesExist = await _dbSet.AsNoTracking().
            AnyAsync(e => e.Id == id, ct);
        return doesExist;
    }
}
