﻿using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    internal class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscountAsync(command, cancellationToken);

            var cart = await basketRepository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(cart.UserName);
        }

        private async Task DeductDiscountAsync(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            // TODO: communicate with Discount.Grpc and calculate lastest prices of products
            foreach (var item in command.Cart.Items)
            {
                var coupon = await discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon?.Amount ?? 0;
            }
        }
    }
}
