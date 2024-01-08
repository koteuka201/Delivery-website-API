using DeliveryAppBack.Models.Order;

namespace DeliveryAppBack.Services
{
    public interface IOrderService
    {
        public Task<OrderCreateDTO> CreateOrder(string token);
        public Task ConfirmOrder(string token, Guid orderId);
        public Task<OrderModel> GetInfoOrder(string token, Guid orderId);
        public Task<List<OrderInfoDTO>> GetListOfOrders(string token);
    }
}
