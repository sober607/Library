using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DAL.EntitiesConfiguration
{
    public class PublishingHouseEntityConfiguration : IEntityTypeConfiguration<PublishingHouse>
    {
        public void Configure(EntityTypeBuilder<PublishingHouse> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(500);

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
