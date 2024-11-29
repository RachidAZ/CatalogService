using Application.Common.EventHandlers;
using Application.Services.Implementation;
using Application.Services.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
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


        // log authentication outcome
        options.Events = new JwtBearerEvents
        {

            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                var claims = context.Principal.Claims;

                // Log claims
                foreach (var claim in claims)
                {
                    Console.WriteLine($"JWT details-->  {claim.Type}: {claim.Value}");
                }

                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync($"Authentication failed: {context.Exception.Message}");

                
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully.");
                var claims = context.Principal.Claims;

                // Log claims
                foreach (var claim in claims)
                {
                    Console.WriteLine($"JWT details--> {claim.Type}: {claim.Value}");
                }
                return Task.CompletedTask;
            }
        };


    }


   
    
    );

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadPolicy", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("Store.Read"))
        ));

    options.AddPolicy("WritePolicy", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("Store.Manage"))
        ));
});


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