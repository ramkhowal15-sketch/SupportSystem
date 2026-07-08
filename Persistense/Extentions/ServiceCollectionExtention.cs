using Application.Interfaces.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistense.DataContext;
using Persistense.Extentions.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistense.Extentions;

public static class ServiceCollectionExtention
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionstring = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDataContext>(opt=>opt.UseSqlServer(connectionstring));
    }

    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
