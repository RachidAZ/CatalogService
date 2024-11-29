using Application.Common.EventHandlers;
using Application.Services.Implementation;
using Application.Services.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters.ValidateAudience = false;

        //options.TokenValidationParameters = new TokenValidationParameters
        //{
        //    ValidateIssuer = true, // Validate the issuer
        //    ValidateAudience = true, // Validate the audience
        //    ValidateLifetime = true, // Ensure token hasn't expired
        //    ValidateIssuerSigningKey = true, // Ensure token signature is valid
        //    ValidIssuer = "https://localhost:5001", // Set your valid issuer
        //    ValidAudience = "https://localhost:5001/resources", // Set your valid audience
        //    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Set your signing key
        //};
    });

// Add Authorization
builder.Services.AddAuthorization();


// Add Infra services
builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("CatalogConnectionString"));

// Add Appliation services (put this inside extension)
builder.Services.AddScoped<IProductService, ProductsServices>();
builder.Services.AddScoped<ICategoryService, CategoriesService>();
builder.Services.AddScoped<ProductPropertyUpdatedEventHandler>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


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


public partial class Program { }