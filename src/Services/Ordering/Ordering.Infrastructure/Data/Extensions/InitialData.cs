namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static Customer[] Customers => [
            Customer.Create(CustomerId.Of(Guid.Parse("a6321246-43ad-48f8-abc1-aa64d4fef223")), "user1", "user1@gmail.com"),
        ];

        public static Product[] Products => [
            Product.Create(ProductId.Of(Guid.Parse("b1de75b6-90e3-44e3-ae73-484ad05f1f4f")), "Iphone X", 1000),
        ];

        public static Order[] OrdersWithItems
        {
            get
            {
                var customer1 = Customers[0];
                var order1 = Order.Create(OrderId.Of(Guid.Parse("b1de75b6-90e3-44e3-ae73-484ad05f1f4f")),
                               customer1.Id,
                               OrderName.Of("ORD_1"),
                               Address.Of("Minh", "Ninh", "cmdev@gmail.com", "Q12, HCM", "Vietnam", "", "70000"),
                               Address.Of("Minh", "Ninh", "cmdev@gmail.com", "Q12, HCM", "Vietnam", "", "70000"),
                               Payment.Of("Ninh Cong Minh", "12345798", "05/2030", "123", 1),
                               OrderStatus.Pending
                           );

                var product1 = Products[0];

                order1.Add(product1.Id, 1, product1.Price);

                return [order1];
            }
        }
    }
}