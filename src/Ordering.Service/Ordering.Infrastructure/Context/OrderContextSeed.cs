using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Context
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetSeedOrders());
                await context.SaveChangesAsync();
                logger.LogInformation($"Seed has been created with context {typeof(OrderContext).Name}");
            }
        }

        private static IEnumerable<Order> GetSeedOrders()
        {
            var orders = new List<Order>();

            for (int i = 0; i < 15; i++)
            {
                orders.Add(new Order { UserName = $"johnDoe{i}", FirstName = $"John{i}", LastName = $"Doe{i}", EmailAddress = $"johndoe{i}@mail.com", AddressLine = $"Boulevard St. {i}", Country = "EUA", TotalPrice = (400 + i), State = "Ilinois", CardName = "Master", CardNumber = "123", CVV = "1234", Expiration = "test", PaymentMethod = 0, ZipCode = "32" });
            }

            for (int i = 0; i < 15; i++)
            {
                orders.Add(new Order { UserName = $"maryDoe{i}", FirstName = $"Mary{i}", LastName = $"Doe{i}", EmailAddress = $"marydoe{i}@mail.com", AddressLine = $"Boulevard St. {i}", Country = "EUA", TotalPrice = (400 + i), State = "Ilinois", CardName = "Master", CardNumber = "123", CVV = "1234", Expiration = "test", PaymentMethod = 0, ZipCode = "32" });
            }

            return orders;
        }
    }
}
