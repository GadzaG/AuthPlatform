using AuthPlatformServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthPlatformServer.Configurations
{
    public class GlobalConfiguration : IEntityTypeConfiguration<GlobalEntity>
    {
        public void Configure(EntityTypeBuilder<GlobalEntity> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(g => g.KeyText)
                .IsRequired();

            builder.Property(g => g.Value)
                .IsRequired();
        }
    }
}
