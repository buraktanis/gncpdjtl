using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.EntitiesBase;
using Core.Results;

namespace Core.Repositories
{ 
    public interface IEntityRepository<T> where T: class,IEntity,new()
    {
        IDataResult<IList<T>> GetList(Expression<Func<T,bool>> filter=null);
        IDataResult<T> Get(Expression<Func<T, bool>> filter = null);
        IDataResult<T> Add(T entity);
        IResult AddRange(IList<T> entityList);
        IResult Update(T entity);
        IResult UpdateAsync(T entity);
        IResult Delete(T entity);
        

    }
}
