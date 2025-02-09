using Domain.Municipalities;
using Domain.TaxSchedules;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Municiaplities;

internal sealed class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
{
    public void Configure(EntityTypeBuilder<Municipality> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(m => m.TaxSchedules)
            .WithOne(t => t.Municipality)
            .HasForeignKey(t => t.MunicipalityId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasData(new Municipality("Copenhagen"));
    }
}
