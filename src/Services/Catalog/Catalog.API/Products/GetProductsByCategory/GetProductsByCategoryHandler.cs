﻿namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByCategoryHandler.Handle called with {@query}", query);

            var products = await session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToListAsync(cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }
}