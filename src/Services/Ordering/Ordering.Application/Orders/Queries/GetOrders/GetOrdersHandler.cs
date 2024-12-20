namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = context.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .OrderBy(x => x.OrderName);

            var count = await query.LongCountAsync(cancellationToken);

            var orders = await query
                .Skip(request.PaginationRequest.PageIndex * request.PaginationRequest.PageSize)
                .Take(request.PaginationRequest.PageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(new PaginatedResult<OrderDto>
            (
                request.PaginationRequest.PageIndex,
                request.PaginationRequest.PageSize,
                count,
                orders.ToOrderDtoList()
            ));
        }
    }
}
