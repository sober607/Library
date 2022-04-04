using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DAL.EntitiesConfiguration
{
    public class BookAuthorEntityConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasOne(d => d.Book)
                .WithMany(t => t.BookAuthors)
                .HasForeignKey(f => f.BookId)
                .HasConstraintName("FK_BooksOfAuthor");

            builder.HasOne(d => d.Author)
                .WithMany(t => t.AuthorBooks)
                .HasForeignKey(f => f.AuthorId)
                .HasConstraintName("FK_AuthorsOfBook");

            builder.HasIndex(f => new { f.AuthorId, f.BookId })
                .IsUnique();

            builder.Property(x => x.BookId)
                .IsRequired();

            builder.Property(x => x.AuthorId)
                .IsRequired();
        }
    }
}
