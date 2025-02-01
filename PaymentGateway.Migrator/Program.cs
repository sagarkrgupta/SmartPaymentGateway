// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Infrastructure.Data;

Console.WriteLine("Migrations Starting....!");

using (var context = new AppDbContext())
{
    context.Database.Migrate();
    Console.WriteLine("Migrations applied successfully.");
}
