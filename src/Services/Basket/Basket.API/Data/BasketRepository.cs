namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var shoppingCart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken)
                ?? throw new BasketNotFoundException(userName);

            return shoppingCart;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);

            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
            // Test ci/cd
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
