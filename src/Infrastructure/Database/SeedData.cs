using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Application.Abstractions.Authentication;

using Domain.Municipalities;
using Domain.TaxSchedules;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public static class SeedData
{

    public static async Task InitializeAsync(ApplicationDbContext dbContext)
    {
        if (await dbContext.Municipalities.AnyAsync())
        {
            return; // DB has been seeded
        }

        await PopulateTestDataAsync(dbContext);
    }

    public static async Task PopulateTestDataAsync(ApplicationDbContext dbContext)
    {

        foreach (var municipality in CreateMunicipalities())
        {
            dbContext.Municipalities.Add(municipality);
        }

        await dbContext.SaveChangesAsync();
    }

    private static List<Municipality> CreateMunicipalities()
    {
        List<Guid> municipalityIds = new List<Guid>
        {
            Guid.NewGuid()
        };

        List<Municipality> municipalities = new List<Municipality>
        {
            new Municipality("Copenhagen")
            {
                Id = municipalityIds[0],
                TaxSchedules = new List<TaxSchedule>
                {
                    new TaxSchedule(
                        municipalityId: municipalityIds[0],
                        taxRate: 0.2m,
                        startDateUtc: new DateTime(2016, 1, 1),
                        endDateUtc: new DateTime(2016, 12, 31),
                        frequency: TaxFrequency.Yearly
                    ),
                    new TaxSchedule(
                        municipalityId: municipalityIds[0],
                        taxRate: 0.4m,
                        startDateUtc: new DateTime(2016, 5, 1),
                        endDateUtc: new DateTime(2016, 5, 31),
                        frequency: TaxFrequency.Monthly
                    ),
                    new TaxSchedule(
                        municipalityId: municipalityIds[0],
                        taxRate: 0.1m,
                        startDateUtc: new DateTime(2016, 1, 1),
                        endDateUtc: new DateTime(2016, 1, 1),
                        frequency: TaxFrequency.Daily
                    ),
                    new TaxSchedule(
                        municipalityId: municipalityIds[0],
                        taxRate: 0.1m,
                        startDateUtc: new DateTime(2016, 12, 25),
                        endDateUtc: new DateTime(2016, 12, 25),
                        frequency: TaxFrequency.Daily
                    )
                }
            }
        };

        return municipalities;
    }
}
