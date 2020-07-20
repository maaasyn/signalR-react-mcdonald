using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


public interface IRepository<TEntity> where TEntity : class

{
    
    TEntity Get(object id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    
    void Insert(TEntity entity);
    void InsertRange(IEnumerable<TEntity> entity);

    void Update(TEntity entityToUpdate);
    
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    void DeleteRange(IEnumerable<TEntity> entities);

    IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");
}