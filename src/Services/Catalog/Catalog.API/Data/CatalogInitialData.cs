using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync(cancellation))
                return;

            session.Store(GetProducts());
            await session.SaveChangesAsync(cancellation);
        }

        private static IEnumerable<Product> GetProducts()
        {
            return [
                new Product {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = ["c1", "c2"],
                    Description = "Description",
                    ImageFile = "ImageFile",
                    Price = 1000,
                },
            ];
        }
    }
}
