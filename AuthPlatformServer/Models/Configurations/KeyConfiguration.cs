using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthPlatformServer.Models;

namespace AuthPlatformServer.Configurations
{
    public class KeyConfiguration : IEntityTypeConfiguration<KeyEntity>
    {
        public void Configure(EntityTypeBuilder<KeyEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(k => k.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(k => k.ProductId)
                .IsRequired();

            builder.HasOne(k => k.Product)
                .WithMany()
                .HasForeignKey(k => k.ProductId);

            builder.Property(k => k.KeyText)
                .IsRequired();

            builder.Property(k => k.ActivationTime)
                .IsRequired(false);

            builder.Property(k => k.Data)
                .HasColumnType("jsonb")
                .IsRequired(false);

            builder.Property(k => k.Status)
                .IsRequired();
        }
    }
}
