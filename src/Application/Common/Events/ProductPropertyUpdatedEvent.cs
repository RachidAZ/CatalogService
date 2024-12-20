using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Common.Events;

public class ProductPropertyUpdatedEvent : IntegrationEventBase
{
    public int ProductId { get; }
    public string Name { get; }
    public string Description { get; }
    public int CategoryId { get; }
    public Money Price { get; }
    public int Amount { get; }




    public ProductPropertyUpdatedEvent
        (int productId, string name, string description, int categoryId, Money price, int amount) : base(nameof(ProductPropertyUpdatedEvent))
    {
        ProductId = productId;
        Name = name;
        Description = description;
        CategoryId = categoryId;
        Price = price;
        Amount = amount;


    }
}