﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PingDong.CleanArchitect.Service;
using PingDong.EventBus.Core;
using PingDong.Newmoon.Places.Core;
using PingDong.Newmoon.Places.Service.IntegrationEvents;

namespace PingDong.Newmoon.Places.Service.DomainEvents
{
    public class PlaceTemporaryClosedDomainEventHandler : EventBusDomainEventHandler, INotificationHandler<PlaceTemporaryClosedDomainEvent>
    {
        public PlaceTemporaryClosedDomainEventHandler(IEventBusPublisher eventBus) 
            : base(eventBus)
        {
        }

        public async Task Handle(PlaceTemporaryClosedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new PlaceTemporaryClosedIntegrationEvent(domainEvent.PlaceId, domainEvent.PlaceName);
            
            await PublishAsync(domainEvent, integrationEvent);
        }
    }
}