using DeliveryAppBack.Models.Busket;

namespace DeliveryAppBack.Services
{
    public interface IBasketService
    {
        public Task<List<BasketModel>> GetUserCart(string token);
        public Task AddDish(string token, Guid dishId);
        public Task DeleteDish(string token, Guid dishId, bool increase);
    }
}
