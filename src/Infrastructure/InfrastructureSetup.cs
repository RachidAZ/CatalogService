using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServerCompact;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Messaging;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;

public static class InfrastructureSetup
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

        services.AddScoped<IRepositoryProduct, RepositoryProduct>();
        services.AddScoped<IMessageBus, RabbitMqEventBus>();
        services.AddScoped<IRepository<Category, int>, RepositoryCategory>();


        return services;
    }

}