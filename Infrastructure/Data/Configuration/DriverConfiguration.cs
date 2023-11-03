using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace produccion.Configuration
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            // Configuración de la entidad
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("drivers");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Age).HasColumnName("age");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            
        }
    }
}