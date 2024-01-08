using DeliveryAppBack.Enums;
using DeliveryAppBack.Models.Busket;

namespace DeliveryAppBack.Models.Order
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }
        public OrderStatusEnum Status { get; set; }
        public int Price { get; set; }
        public List<BasketModel> Dishes { get; set; }
        public string Address { get; set; }
    }
}
