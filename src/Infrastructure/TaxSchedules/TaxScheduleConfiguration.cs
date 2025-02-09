using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.TaxSchedules;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.TaxSchedules;

internal sealed class TaxScheduleConfiguration : IEntityTypeConfiguration<TaxSchedule>
{
    public void Configure(EntityTypeBuilder<TaxSchedule> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TaxRate)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(t => t.StartDateUtc)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(t => t.EndDateUtc)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(t => t.Frequency)
            .IsRequired()
            .HasConversion<int>();

        //builder.HasData(
        //    new TaxSchedule(Guid.NewGuid(), 0.2m, new DateTime(2016, 01, 01), new DateTime(2016, 12, 31), TaxFrequency.Yearly),
        //    new TaxSchedule(Guid.NewGuid(), 0.4m, new DateTime(2016, 05, 01), new DateTime(2016, 05, 31), TaxFrequency.Monthly),
        //    new TaxSchedule(Guid.NewGuid(), 0.1m, new DateTime(2016, 01, 01), new DateTime(2016, 01, 01), TaxFrequency.Daily),
        //    new TaxSchedule(Guid.NewGuid(), 0.1m, new DateTime(2016, 12, 25), new DateTime(2016, 12, 25), TaxFrequency.Daily)
        //);
    }
}
