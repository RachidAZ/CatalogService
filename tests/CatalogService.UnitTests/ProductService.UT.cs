using Application.Services.Interfaces;
using Application.Services.Implementation;
using Domain.Entities;
using Moq;

namespace CatalogService.UnitTests;

public class ProductServiceTest
{
    [Fact]
    public void DeteleProduct_ShouldReturnFalse_WhenProductNotFound()
    {
        // Arrange
        var repo_mock = new Mock<IRepositoryProduct>();
        repo_mock.Setup(m=> m.Delete(It.IsAny<int>())).Throws<Exception>();
        var eventHandler = new Application.Common.EventHandlers.ProductPropertyUpdatedEventHandler(null);
        IProductService productService = new ProductsServices(repo_mock.Object, eventHandler);


        //Act

        var res = productService.DeleteProduct(123);

        //Assert
        Assert.False(res.IsSuccess);

    }


    [Fact]
    public void DeteleProduct_ShouldReturnTrue_WhenProductFound()
    {

        // Arrange
        var repo_mock = new Mock<IRepositoryProduct>();
        repo_mock.Setup(m => m.GetByKey(It.IsAny<int>())).Returns(new Product());

        var eventHandler = new Application.Common.EventHandlers.ProductPropertyUpdatedEventHandler(null);
        IProductService productService = new ProductsServices(repo_mock.Object, eventHandler);


        //Act

        var res = productService.DeleteProduct(123);

        //Assert
        Assert.True(res.IsSuccess);

    }

}