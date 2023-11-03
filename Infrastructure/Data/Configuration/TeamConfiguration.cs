using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Core.Entities;

namespace produccion.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            // Configuración de la entidad
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("teams");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            builder.HasMany(e => e.Drivers)
                .WithMany(r => r.Teams)
                .UsingEntity<team_drivers>(
                    j =>
                    {
                        j.HasOne(p => p.Driver)
                         .WithMany(p => p.Team_Drivers)
                         .HasForeignKey(p => p.Team_Id);
            
                        j.HasOne(e => e.Team)
                         .WithMany(e => e.Team_Drivers)
                         .HasForeignKey(e => e.Driver_Id);
            
                        j.ToTable("team_drivers");
                        j.HasKey(t => new { t.Team_Id, t.Driver_Id });
                    });
        }
    }
}