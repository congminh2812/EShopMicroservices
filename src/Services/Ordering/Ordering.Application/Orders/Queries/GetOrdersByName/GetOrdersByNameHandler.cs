
namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {
            var orders = await context.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .Where(x => x.OrderName.Value.Contains(request.Name))
                .OrderBy(x => x.OrderName)
                .ToListAsync(cancellationToken);

            //var orderDtos = orders.Adapt<List<OrderDto>>();

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
