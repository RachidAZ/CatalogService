using Application.Common.Events;
using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Common.EventHandlers;

public class ProductPropertyUpdatedEventHandler
{

    public readonly IMessageBus _messageBus;

    public ProductPropertyUpdatedEventHandler(IMessageBus messageBus)
    {
        this._messageBus = messageBus;
    }

    public  async Task Handle(ProductPropertyUpdatedEvent @event){

        await _messageBus.PublishAsync("catalog",@event);


    }

}
