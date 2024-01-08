using DeliveryAppBack.Enums;
using DeliveryAppBack.Models.Busket;

namespace DeliveryAppBack.Models.Order
{
    public class OrderInfoDTO
    {
        public Guid Id { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }
        public OrderStatusEnum Status { get; set; }
        public int Price { get; set; }
    }
}
