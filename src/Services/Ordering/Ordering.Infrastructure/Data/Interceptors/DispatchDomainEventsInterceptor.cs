using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            DispatchDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            DispatchDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEventsAsync(DbContext? context)
        {
            if (context is null) return;

            var aggregates = context.ChangeTracker.Entries<IAggregate>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity);

            var domainEvents = aggregates.SelectMany(x => x.DomainEvents).ToList();

            aggregates.ToList().ForEach(x => x.ClearDomainEvents());

            foreach (var item in domainEvents)
                await mediator.Publish(item);
        }
    }
}
