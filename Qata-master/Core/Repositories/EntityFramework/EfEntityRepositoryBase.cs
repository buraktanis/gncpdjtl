using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.EntitiesBase;
using Core.Extensions;
using Core.Results;
using Microsoft.EntityFrameworkCore;


namespace Core.Repositories.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {

        
        public IDataResult<TEntity> Add(TEntity entity)
        {
            using (var context=new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return new SuccessDataResult<TEntity>(entity);
            }
        }

        public IResult AddRange(IList<TEntity> entityList)
        {
            using var context = new TContext();
            context.AddRange(entityList);
            context.SaveChanges();
            return new SuccessResult();
        }

        public IResult Delete(TEntity entity)
        {
            using (var context = new TContext()) 
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }

            return new SuccessResult();
        }

        public IDataResult<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context =new TContext())
            {
                return new SuccessDataResult<TEntity>(context.Set<TEntity>().FirstOrDefault(filter));
            }
        }

        public IDataResult<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();
            return new SuccessDataResult<IList<TEntity>>(filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList());
        }

        public IResult Update(TEntity entity)
        {
            using var context= new TContext();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
            return new SuccessResult();
        } 
        public IResult UpdateAsync(TEntity entity)
        {
            using var context= new TContext();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChangesAsync();
            return new SuccessResult();
        }
    }
}
