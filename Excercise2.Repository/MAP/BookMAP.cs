using Excercise2.Repository.EntityClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise2.Repository.MAP
{
    internal class BookMAP : IEntityTypeConfiguration<Book>
    {

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK_Book");

            builder.Property(e => e.AuthorFirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.AuthorLastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Price);

            builder.Property(e => e.Publisher)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
