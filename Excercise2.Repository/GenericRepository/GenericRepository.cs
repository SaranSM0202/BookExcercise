using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Excercise2.Repository.GenericRepository
{
    /// <summary>
    /// class to handle all database operations 
    /// </summary>
    /// <typeparam name="EntityCLS"></typeparam>
    public class Repository<EntityCLS> : IGenericRepository<EntityCLS> where EntityCLS : class
    {
        public Repository(DbContext p_dbContext)
        {
            _context = p_dbContext;
            _dbSet = _context.Set<EntityCLS>();
        }


        #region GET
        /// <summary>
        /// Get list of entities values based on predicate and inclusion properties
        /// </summary>
        /// <param name="p_predicate"></param>
        /// <param name="p_firstInclusion"></param>
        /// <param name="p_secondInclusion"></param>
        /// <param name="p_thirdInclusion"></param>
        /// <param name="p_fourthInclusion"></param>
        /// <returns></returns>
        public async Task<IList<EntityCLS>> GetListasync(Expression<Func<EntityCLS, bool>> p_predicate = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_firstInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_secondInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_thirdInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_fourthInclusion = null)
        {
            IQueryable<EntityCLS> queryable = _dbSet;
            if (p_firstInclusion != null)
                queryable = p_firstInclusion(queryable);
            if (p_secondInclusion != null)
                queryable = p_secondInclusion(queryable);
            if (p_thirdInclusion != null)
                queryable = p_thirdInclusion(queryable);
            if (p_fourthInclusion != null)
                queryable = p_fourthInclusion(queryable);

            //Construct list based on predicate
            if (p_predicate != null)
                queryable = queryable.Where(p_predicate);

            return await queryable.ToListAsync();

        }

        /// <summary>
        ///  Get enumerable list of entities values based on predicate and inclusion properties
        /// </summary>
        /// <param name="p_predicate"></param>
        /// <param name="p_firstInclusion"></param>
        /// <param name="p_secondInclusion"></param>
        /// <param name="p_thirdInclusion"></param>
        /// <param name="p_fourthInclusion"></param>
        /// <returns></returns>
        public IEnumerable<EntityCLS> GetEnumerableasync(Expression<Func<EntityCLS, bool>> p_predicate = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_firstInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_secondInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_thirdInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_fourthInclusion = null)
        {
            IQueryable<EntityCLS> queryable = _dbSet;
            if (p_firstInclusion != null)
                queryable = p_firstInclusion(queryable);
            if (p_secondInclusion != null)
                queryable = p_secondInclusion(queryable);
            if (p_thirdInclusion != null)
                queryable = p_thirdInclusion(queryable);
            if (p_fourthInclusion != null)
                queryable = p_fourthInclusion(queryable);

            //Construct list based on predicate
            if (p_predicate != null)
                queryable = queryable.Where(p_predicate);

            return queryable.AsEnumerable();
        }

        /// <summary>
        /// Get single entity value based on predicate and inclusion properties
        /// </summary>
        /// <param name="p_predicate"></param>
        /// <param name="p_firstInclusion"></param>
        /// <param name="p_secondInclusion"></param>
        /// <param name="p_thirdInclusion"></param>
        /// <param name="p_fourthInclusion"></param>
        /// <returns></returns>
        public async Task<EntityCLS> GetSingleasync(Expression<Func<EntityCLS, bool>> p_predicate = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_firstInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_secondInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_thirdInclusion = null, Func<IQueryable<EntityCLS>, IIncludableQueryable<EntityCLS, object>> p_fourthInclusion = null)
        {
            IQueryable<EntityCLS> queryable = _dbSet;
            if (p_firstInclusion != null)
                queryable = p_firstInclusion(queryable);
            if (p_secondInclusion != null)
                queryable = p_secondInclusion(queryable);
            if (p_thirdInclusion != null)
                queryable = p_thirdInclusion(queryable);
            if (p_fourthInclusion != null)
                queryable = p_fourthInclusion(queryable);

            //Construct list based on predicate
            if (p_predicate != null)
                queryable = queryable.Where(p_predicate);

            return await queryable.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<EntityCLS> FindByIDasync(object p_id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(p_id);
        }

        public async Task<EntityCLS> FindByQuery(Expression<Func<EntityCLS, bool>> p_query, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(p_query, cancellationToken);
        }

        #endregion

        #region Insert
        /// <summary>
        /// Insert Multiple Entities,Cancellation -to observe while waiting for the task to complete
        /// </summary>
        /// <param name="p_entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response> Insert(EntityCLS p_entity)
        {
            Response response = Success();
            try
            {
                _dbSet.Add(p_entity);
                response.Data = p_entity;
            }
            catch (Exception e)
            {
                response.Exception(e);

            }
            return response;
        }

        public async Task<Response> InsertMultiple(List<EntityCLS> p_entityList, CancellationToken cancellationToken = default)
        {
            Response response = Success();
            try
            {
                await _dbSet.AddRangeAsync(p_entityList, cancellationToken);
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }
        /// <summary>
        ///  Insert Multiple Entities,Cancellation -to observe while waiting for the task to complete
        /// </summary>
        /// <param name="p_entityList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Response> InsertMultiple(IEnumerable<EntityCLS> p_entityList, CancellationToken cancellationToken = default)
        {
            Response response = Success();
            try
            {
                await _dbSet.AddRangeAsync(p_entityList, cancellationToken);
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }

        #endregion


        #region Update

        /// <summary>
        /// Update single entity
        /// </summary>
        /// <param name="p_entity"></param>
        /// <returns></returns>
        public Response Update(EntityCLS p_entity)
        {
            Response response = Success();
            try
            {

                _dbSet.Update(p_entity);
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }

        /// <summary>
        /// Update multiple enumerable entities
        /// </summary>
        /// <param name="p_entityList"></param>
        /// <returns></returns>
        public Response UpdateMultiple(IEnumerable<EntityCLS> p_entityList)
        {
            Response response = Success();
            try
            {
                _dbSet.UpdateRange(p_entityList);
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }

        /// <summary>
        /// Update mulitiple list of entities
        /// </summary>
        /// <param name="p_entityList"></param>
        /// <returns></returns>
        public Response UpdateMultiple(List<EntityCLS> p_entityList)
        {
            Response response = Success();
            try
            {
                _dbSet.UpdateRange(p_entityList);
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete specific entity
        /// </summary>
        /// <param name="p_entity"></param>
        /// <returns></returns>
        public Response Delete(EntityCLS p_entity)
        {
            Response response = Success();
            try
            {
                _dbSet.RemoveRange(p_entity);
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }


        /// <summary>
        /// Delete specific entity also stub entity to mark for deletion
        /// </summary>
        /// <param name="p_id"></param>
        /// <returns></returns>
        public Response Delete_ByID(object p_id)
        {
            Response response = Success();
            try
            {
                //stub deletion
                var type = typeof(EntityCLS).GetTypeInfo();
                var key = _context.Model.FindEntityType(type).FindPrimaryKey().Properties.FirstOrDefault();
                var prop = type.GetProperty(key.Name);
                if (prop != null)
                {
                    var entity = Activator.CreateInstance<EntityCLS>();
                    prop.SetValue(entity, p_id);
                    _context.Entry(entity).State = EntityState.Deleted;
                }
                else
                {

                    EntityCLS entityobj = _dbSet.Find(p_id);
                    if (null != entityobj)
                        _dbSet.Remove(entityobj);
                }
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }

        /// <summary>
        ///Delete multiple collections by its primarykey
        /// </summary>
        /// <param name="p_entityID"></param>
        /// <returns></returns>
        public Response Delete_MultipleItems(List<long> p_entityID)
        {
            Response response = Success();
            try
            {
                if (p_entityID != null && p_entityID.Count > 0)
                {
                    string errmsg = string.Empty;
                    p_entityID.ForEach(id =>
                    {
                        var singleitemresponse = this.Delete_ByID(id);
                        if (singleitemresponse.IsError)
                            errmsg += singleitemresponse.Message;
                    });
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        response.Exception();
                        response.Message = errmsg;
                    }
                }

            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }

        /// <summary>
        /// Delete multiple collections
        /// </summary>
        /// <param name="p_entityList"></param>
        /// <returns></returns>
        public Response Delete_MultipleItems(IEnumerable<EntityCLS> p_entityList)
        {
            Response response = Success();
            try
            {
                _dbSet.RemoveRange(p_entityList);
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }
        /// <summary>
        ///Delete multiple collections by its primarykey
        /// </summary>
        /// <param name="p_entityID"></param>
        /// <returns></returns>
        public Response Delete_MultipleItems(List<int> p_entityID)
        {
            Response response = Success();
            try
            {
                if (p_entityID != null && p_entityID.Count > 0)
                {
                    string errmsg = string.Empty;
                    p_entityID.ForEach(id =>
                    {
                        var singleitemresponse = this.Delete_ByID(id);
                        if (singleitemresponse.IsError)
                            errmsg += singleitemresponse.Message;
                    });
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        response.Exception();
                        response.Message = errmsg;
                    }
                }

            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }


        /// <summary>
        /// Delete multiple Entries 
        /// </summary>
        /// <param name="p_entityList"></param>
        /// <returns></returns>
        public Response Delete_MultipleItems(List<EntityCLS> p_entityList)
        {
            Response response = Success();
            try
            {
                _dbSet.RemoveRange(p_entityList);
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }

        /// <summary>
        /// Delete multiple Entries 
        /// </summary>
        /// <param name="p_entityList"></param>
        /// <returns></returns>
        public Response Delete_MultipleItems(IList<EntityCLS> p_entityList)
        {
            Response response = Success();
            try
            {
                _dbSet.RemoveRange(p_entityList);
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }
        #endregion

        #region SPExecution
        public IQueryable<EntityCLS> FromSql(string p_sql, params object[] p_parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSP(string p_sql, params object[] p_parameters)
        {
            throw new NotImplementedException();
        }
        public Response Success()
        {
            return new Response() { Message = Status.Success.ToString(), StatusCode = Convert.ToInt32(Status.Success) };
        }


        #endregion



        //private members
        private readonly DbContext _context;
        private readonly DbSet<EntityCLS> _dbSet;
    }
}
