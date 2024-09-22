using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthPlatformServer.Models;


namespace AuthPlatformServer.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(p => p.Title)
                .IsRequired();

            builder.Property(p => p.Period)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired(false);
        }
    }
}
