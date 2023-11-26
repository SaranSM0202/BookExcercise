using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Excercise2.Repository.GenericRepository
{
    public interface IGenericRepository<EntityCLS> where EntityCLS : class
    {
        //GET
        Task<IList<EntityCLS>> GetListasync(Expression<Func<EntityCLS, bool>> p_predicate = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_firstInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_secondInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_thirdInclusion = null,
            Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_fourthInclusion = null);
        IEnumerable<EntityCLS> GetEnumerableasync(Expression<Func<EntityCLS, bool>> p_predicate = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_firstInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_secondInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_thirdInclusion = null,
            Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_fourthInclusion = null);


        //Single 
        Task<EntityCLS> GetSingleasync(Expression<Func<EntityCLS, bool>> p_predicate = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_firstInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_secondInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_thirdInclusion = null,
            Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_fourthInclusion = null);

        //find individual items
        Task<EntityCLS> FindByIDasync(object p_id, CancellationToken cancellationToken = default(CancellationToken));
        Task<EntityCLS> FindByQuery(Expression<Func<EntityCLS, bool>> p_query, CancellationToken cancellationToken = default(CancellationToken));

        //Insert
        Task<Response> Insert(EntityCLS p_entity);
        Task<Response> InsertMultiple(List<EntityCLS> p_entityList, CancellationToken cancellationToken = default(CancellationToken));
        Task<Response> InsertMultiple(IEnumerable<EntityCLS> p_entityList, CancellationToken cancellationToken = default(CancellationToken));

        //Update
        Response Update(EntityCLS p_entity);
        Response UpdateMultiple(IEnumerable<EntityCLS> p_entityList);
        Response UpdateMultiple(List<EntityCLS> p_entityList);


        //collection
        Response Delete_MultipleItems(List<EntityCLS> p_entityList);
        Response Delete_MultipleItems(List<int> p_entityID);
        Response Delete_MultipleItems(IList<EntityCLS> p_entityList);

        Response Delete_ByID(object p_entityID);
        Response Delete(EntityCLS p_entity);
        Response Delete_MultipleItems(List<long> p_entityID);

        /// <summary>
        /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        IQueryable<EntityCLS> FromSql(string p_sql, params object[] p_parameters);

        Task<int> ExecuteSP(string p_sql, params object[] p_parameters);

    }
}
