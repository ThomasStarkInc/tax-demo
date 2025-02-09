using System;
using System.Runtime.CompilerServices;

using Application.Abstractions.Authentication;

using Domain.Municipalities;
using Domain.Schedules;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public static class SeedData
{
    public static readonly Municipality[] Municipalities = new Municipality[]
    {
                    new Municipality("Copenhagen")
                    {
                       
                    }
    };

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
        foreach (var municipality in Municipalities)
        {
            dbContext.Municipalities.Add(municipality);
        }

        await dbContext.SaveChangesAsync();
    }
}
