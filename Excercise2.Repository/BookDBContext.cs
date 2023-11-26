using Excercise2.Repository.EntityClass;
using Microsoft.EntityFrameworkCore;
using Excercise2.Repository.MAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2.Repository
{
    public partial class BookDBContext : DbContext
    {
        public BookDBContext()
        {
        }

        public BookDBContext(DbContextOptions<BookDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Book> Book { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration<Book>(new BookMAP());
        }
    }
}
