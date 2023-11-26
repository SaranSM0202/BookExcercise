using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2.Repository.GenericRepository
{
    /// <summary>
    /// Defines the interface(s) for unit of work. 
    /// </summary>
    public interface IUnitOfWork<Context> : IDisposable where Context : DbContext
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();

        Context CurrentContext { get; }

    }
}
