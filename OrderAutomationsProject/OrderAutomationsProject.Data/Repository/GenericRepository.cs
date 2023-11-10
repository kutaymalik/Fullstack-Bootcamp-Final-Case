using Microsoft.EntityFrameworkCore;
using OrderAutomationsProject.Base.Model;
using OrderAutomationsProject.Data.Context;
using System.Linq.Expressions;

namespace OrderAutomationsProject.Data.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
{
    private readonly OaDbContext dbContext;
    public GenericRepository(OaDbContext dbcontext)
    {
        this.dbContext = dbcontext;
    }

    // Soft delete (State changing to passive)
    public void Delete(TEntity entity)
    {
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        entity.UpdatedById = 1;
        dbContext.Set<TEntity>().Update(entity);
    }

    public void DeleteById(int id)
    {
        var entity = dbContext.Set<TEntity>().Find(id);
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        entity.UpdatedById = 1;
        dbContext.Set<TEntity>().Update(entity);
    }

    public List<TEntity> GetAll(params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();

        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        return query.ToList();
    }

    public IQueryable<TEntity> GetAsQueryable(params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();

        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        return query;
    }

    public TEntity GetById(int id, params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();

        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return query.FirstOrDefault(x => x.Id == id);
    }

    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken, params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();

        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.InsertDate = DateTime.UtcNow;
        entity.CreatedById = 1;
        entity.IsActive = true;
        await dbContext.Set<TEntity>().AddAsync(entity);
    }

    public void Insert(TEntity entity, CancellationToken cancellationToken)
    {
        entity.InsertDate = DateTime.UtcNow;
        entity.CreatedById = 1;
        dbContext.Set<TEntity>().Add(entity);
    }

    public void InsertRange(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.CreatedById = 1;
            x.InsertDate = DateTime.UtcNow;
            x.IsActive = true;
        });
        dbContext.Set<TEntity>().AddRange(entities);
    }

    // Hard Delete (Deleting from db)
    public void Remove(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public void RemoveById(int id)
    {
        var entity = dbContext.Set<TEntity>().Find(id);
        dbContext.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    IEnumerable<TEntity> IGenericRepository<TEntity>.Where(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();

        if (expression != null)
        {
            query = query.Where(expression);
        }


        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return query.ToList();
    }

    TEntity IGenericRepository<TEntity>.FirstOrDefault(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();

        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return query.FirstOrDefault();
    }
}
