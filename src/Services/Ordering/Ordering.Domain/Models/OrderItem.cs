using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}