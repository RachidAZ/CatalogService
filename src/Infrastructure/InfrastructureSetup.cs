﻿using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServerCompact;
using Microsoft.EntityFrameworkCore;
using Application.Services.Interfaces;
using Infrastructure.Messaging;


namespace Infrastructure;

public static class InfrastructureSetup
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
        
        services.AddScoped<IRepositoryProduct , RepositoryProduct>();
        services.AddScoped<IMessageBus , RabbitMqEventBus>();
        services.AddScoped<IRepository<Category, int> , RepositoryCategory>();
        

        return services;
    }

}
