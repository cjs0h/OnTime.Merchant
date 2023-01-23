using System;
using System.Collections.Generic;
using System.Linq;
using Merchant.Common.Extensions;
using Merchant.Domain.Entities;
using Merchant.Persistence.Data;

namespace Merchant.Tests;

public class DbInitializer
{
    public static void Initialize(EfContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        Seed(context);
    }

    private static void Seed(EfContext context)
    {
        // Seed additional data according to your application here
        var appConfigs = new List<User>
        {
            new User
            {
                Id = Guid.Parse("cbc10c4e-1c9d-4e8a-8b8f-a66cfc040450"),
                UserName = "username1",
                Password = "password1".HashPassword(),
                FirstName = "firstname1",
                LastName = "lastname1",
                Email = "test1@test.com",
                PhoneNumber = "1234567891",
                IsActive = true
            },
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "username2",
                Password = "password2".HashPassword(),
                FirstName = "firstname2",
                LastName = "lastname2",
                Email = "test2@test.com",
                PhoneNumber = "1234567892"
            },
            new User
            {
                Id = Guid.Parse("ebc10b4e-1c9d-4e0a-8b8f-a66cfc010451"),
                UserName = "username3",
                Password = "password3".HashPassword(),
                FirstName = "firstname3",
                LastName = "lastname3",
                Email = "test3@test.com",
                PhoneNumber = "1234567893"
            },
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "username4",
                Password = "password4".HashPassword(),
                FirstName = "firstname4",
                LastName = "lastname4",
                Email = "test4@test.com",
                PhoneNumber = "1234567894"
            }
        };

        context.Users.AddRange(appConfigs);
        context.SaveChanges();
    }
}