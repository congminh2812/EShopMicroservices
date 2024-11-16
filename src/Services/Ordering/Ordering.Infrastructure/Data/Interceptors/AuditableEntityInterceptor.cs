using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context is null) return;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = "admin";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State is (EntityState.Added or EntityState.Modified))
                {
                    entry.Entity.LastModifiedBy = "admin";
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                }
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) => entry.References.Any(x => x.TargetEntry != null && x.TargetEntry.Metadata.IsOwned() && (x.TargetEntry.State is (EntityState.Added or EntityState.Modified)));
}