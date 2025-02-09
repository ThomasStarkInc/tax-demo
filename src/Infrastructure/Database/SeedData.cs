using System;

using Application.Abstractions.Authentication;

using Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public static class SeedData
{
    public static readonly User[] Users = new User[]
    {
        new User()
        {
            Id = Guid.NewGuid(),
            Email = "john@doe.com",
            FirstName = "John",
            LastName = "Doe",
            PasswordHash = "hellohello"
        },
        new User()
        {
            Id = Guid.NewGuid(),
            Email = "snow@frog.com",
            FirstName = "Snow",
            LastName = "Frog",
            PasswordHash = "worldworld"
        }
    };

    public static async Task InitializeAsync(ApplicationDbContext dbContext, IPasswordHasher passwordHasher)
    {
        if (await dbContext.Users.AnyAsync())
        {
            return; // DB has been seeded
        }

        await PopulateTestDataAsync(dbContext, passwordHasher);
    }

    public static async Task PopulateTestDataAsync(ApplicationDbContext dbContext, IPasswordHasher passwordHasher)
    {
        foreach (var user in Users)
        {
            user.PasswordHash = passwordHasher.Hash(user.PasswordHash);
        }

        dbContext.Users.AddRange(Users);
        await dbContext.SaveChangesAsync();
    }
}
