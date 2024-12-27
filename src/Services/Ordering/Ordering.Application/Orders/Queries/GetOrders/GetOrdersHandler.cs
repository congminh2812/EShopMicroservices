namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var pageIndex = request.PaginationRequest.PageIndex;
            var pageSize = request.PaginationRequest.PageSize;

            var query = context.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .OrderBy(x => x.OrderName.Value);

            var count = await query.LongCountAsync(cancellationToken);
            var skip = (pageIndex - 1) * pageSize;

            var orders = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(new PaginatedResult<OrderDto>
            (
                pageIndex,
                pageSize,
                count,
                orders.ToOrderDtoList()
            ));
        }
    }
}
