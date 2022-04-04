using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DAL.EntitiesConfiguration
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
                builder.Property(x => x.Title)
                .IsRequired();

                builder.Property(x => x.Title)
                .HasMaxLength(1000);
        }
    }
}
