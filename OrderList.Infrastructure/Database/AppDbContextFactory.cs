using Microsoft.Extensions.Configuration;

namespace OrderList.Infrastructure.Database;

public class AppDbContextFactory(IConfiguration configuration)
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");
    private readonly string _databaseName = configuration["DatabaseName"];

    public AppDbContext Create()
    {
        return new AppDbContext(_connectionString, _databaseName);
    }
}