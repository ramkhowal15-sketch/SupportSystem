using Application.Interfaces.Services.MailServices;
using Application.Interfaces.Services.TokenServices;
using Infrastracture.Services.MailServicves;
using Infrastracture.Services.TokenServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.Services;

public static class ServiceCollectionExtention
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddService();
    }
    public static void AddService(this IServiceCollection services)
    {
        services
            .AddScoped<IMailService, MailService>()
            .AddScoped<ITokenService, TokenService>();
            
    }
}
