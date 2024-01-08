namespace DeliveryAppBack.Models.Order
{
    public class TempHistory
    {
        public Guid Id { get; set; }
        public Guid DishId { get; set; }
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
    }
}
