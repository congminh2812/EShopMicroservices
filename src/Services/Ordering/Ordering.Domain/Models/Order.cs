using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<Guid>
    {
        private static readonly List<OrderItem> _orderItems = [];
        public IReadOnlyList<OrderItem> OrderItems = _orderItems.AsReadOnly();

        public Guid CustomerId { get; set; }
        public string OrderName { get; set; } = default!;
        public Address ShippingAddress { get; set; } = default!;
        public Address BillingAddress { get; set; } = default!;
        public Payment Payment { get; set; } = default!;
        public OrderStatus Status { get; set; }
        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }
    }
}
