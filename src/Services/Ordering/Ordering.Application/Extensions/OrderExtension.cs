﻿namespace Ordering.Application.Extensions
{
    public static class OrderExtension
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders) 
            => orders.Select(order => new OrderDto(
                        Id: order.Id.Value,
                        CustomerId: order.CustomerId.Value,
                        OrderName: order.OrderName.Value,
                        ShippingAddress: new AddressDto(
                                order.ShippingAddress.FirstName,
                                order.ShippingAddress.LastName,
                                order.ShippingAddress.EmailAddress,
                                order.ShippingAddress.AddressLine,
                                order.ShippingAddress.Country,
                                order.ShippingAddress.State,
                                order.ShippingAddress.ZipCode
                            ),
                        BillingAddress: new AddressDto(
                            order.BillingAddress.FirstName,
                            order.BillingAddress.LastName,
                            order.BillingAddress.EmailAddress,
                            order.BillingAddress.AddressLine,
                            order.BillingAddress.Country,
                            order.BillingAddress.State,
                            order.BillingAddress.ZipCode
                        ),
                        Payment: new PaymentDto(
                            order.Payment.CardName,
                            order.Payment.CardNumber,
                            order.Payment.Expiration,
                            order.Payment.Cvv,
                            order.Payment.PaymentMethod
                        ),
                        Status: order.Status,
                        OrderItems: order.OrderItems.Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price)).ToList()
                    ));
    }
}
