using Application.Interfaces;
using Application.UseCases;
using Infrastructure;
using Infrastructure.Persistence;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Infra services
builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("CatalogConnectionString"));

// Add Appliation services (put this inside extension)
builder.Services.AddScoped<IProductService, ManageProducts>();
builder.Services.AddScoped<ICategoryService, ManageCategories>();


var app = builder.Build();


// Ensure the database is created or migrated - move to migrations once issue fixed
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();  
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
