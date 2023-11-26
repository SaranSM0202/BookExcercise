using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excercise2.Repository.GenericRepository
{
    /// <summary>
    /// class to represent all db related operation (ORM operations)
    /// </summary>
    /// <typeparam name="Context"></typeparam>
    /// <typeparam name="UtilityFactories"></typeparam>
    public class Unitofwork<Context> : IUnitOfWork<Context> where Context : DbContext  
    {
        //constructor
        public Unitofwork(Context p_dbContext)
        {
            _dbContext = p_dbContext ?? throw new ArgumentNullException(nameof(p_dbContext));
        }


        #region IGenericRepository

        /// <summary>
        /// Gets the specified repository for the TENTITY
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<TEntity>(_dbContext);

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        #endregion

        #region IUnit

        /// <summary>
        /// Get current dbcontext
        /// </summary>
        public Context CurrentContext => _dbContext;



        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges() { return _dbContext.SaveChanges(); }


        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the database
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync() { return await _dbContext.SaveChangesAsync(); }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // clear repositories
                    if (_repositories != null)
                    {
                        _repositories.Clear();
                    }

                    // dispose the db context.
                    _dbContext.Dispose();
                }
            }

            _isDisposed = true;
        }


        #endregion

        //private members
        private readonly Context _dbContext;
        private bool _isDisposed = false;
        private Dictionary<Type, object> _repositories;

    }
}
