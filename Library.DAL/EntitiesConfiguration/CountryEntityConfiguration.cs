using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DAL.EntitiesConfiguration
{
    public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50);

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
