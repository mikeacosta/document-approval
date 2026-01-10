
using DocumentApproval.Application.Abstractions;
using DocumentApproval.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentApproval.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        // EF Core DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        // Unit of Work
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        // Repositories
        services.AddScoped<IDocumentRepository, DocumentRepository>();

        return services;
    }
}