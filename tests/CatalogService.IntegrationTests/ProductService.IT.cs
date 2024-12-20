using Application.Services.Interfaces;
using Application.Services.Implementation;
using Domain.Entities;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace CatalogService.IntegrationTests;

public class ProductServiceIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>, IAsyncLifetime
{

    private readonly HttpClient _client;
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceScope _scope;

    public ProductServiceIntegrationTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();

        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    public async Task InitializeAsync()
    {
        // Clear the database before each test
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();

    }

    private void DataSeed_AddProduct(int nbrProduct)
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                for (int i = 1; i <= nbrProduct; i++)
                {



                    _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products ON");
                    _dbContext.Products.Add(new Product() { Id = i, Name = "testpr" });
                    _dbContext.SaveChanges();
                    _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products OFF");


                }
                transaction.Commit();


            }
            catch (Exception ex)
            {
                // Rollback transaction if there's an error
                transaction.Rollback();
                Console.WriteLine(ex);
                throw;
            }
        }
    }



    [Fact]
    public async Task GetProduct_ShouldReturn200Ok_WhenProductIsFound()
    {
        // Arrange and Act
        int productId = 1;
        DataSeed_AddProduct(1);
        var response = await _client.GetAsync($"/Product/GetProduct/{productId}");



        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        result.Should().Contain("name")
               .And.Contain("description");

    }

    [Fact]
    public async Task GetProduct_ShouldReturnUnfound_WhenProductIsUnFound()
    {
        // Arrange and Act
        int productId = 123;
        var response = await _client.GetAsync($"/Product/GetProduct/{productId}");



        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }

    [Fact]
    public async Task GetProducts_ShouldReturnAllProducts()
    {
        // Arrange and Act        
        DataSeed_AddProduct(10);
        // /add_product
        var response = await _client.GetAsync("/Product/GetListProducts");



        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        result.Should().Contain("description")
               .And.Contain("name");




    }
    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }
}
