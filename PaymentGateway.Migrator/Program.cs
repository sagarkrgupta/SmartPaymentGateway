using Microsoft.Extensions.Configuration;


// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Migrations Starting....!");

// Load configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Get the connection string
var connectionString = configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Error: Connection string is missing in appsettings.json.");
    return;
}


// Configure services
var services = new ServiceCollection();
services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(connectionString));

var serviceProvider = services.BuildServiceProvider();

// Resolve the AppDbContext and apply migrations
using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        Console.WriteLine("Applying migrations...");
        dbContext.Database.Migrate();
        Console.WriteLine("Migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
    }
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
