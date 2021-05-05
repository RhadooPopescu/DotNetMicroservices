using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    //This interface handles database related actions.
    //This will be our generic repository interface and will be implemented under the infrastructure layer namely RepositoryBase class.
    public interface IAsyncRepository<T> where T : EntityBase
    {
        //This method expects all the data from the entitie provided in the type<T> object namely our EntityBase.
        Task<IReadOnlyList<T>> GetAllAsync();

        //This method takes the expression as a function inside the predicate parameter that filters our entities.
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        //This method applies the configurations for queries into the entity framework core.
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);

        //This method applies the configurations for queries into the entity framework core, and defines the predicate expresion as a function format.
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       List<Expression<Func<T, object>>> includes = null,
                                       bool disableTracking = true);

        //This method returns the information for the given id.
        Task<T> GetByIdAsync(int id);

        //The following three methods are performing the CRUD operations.
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
