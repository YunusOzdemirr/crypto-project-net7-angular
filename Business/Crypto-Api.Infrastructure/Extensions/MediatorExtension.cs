using System;
using MediatR;
using Crypto_Api.Domain.Common;
using Crypto_Api.Infrastructure.Context;

namespace Crypto_Api.Infrastructure.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, MicroServiceContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                                    .Entries<BaseEntity>()
                                    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}

