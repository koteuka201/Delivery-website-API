namespace DeliveryAppBack.Models.Busket
{
    public class DishBasketDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid DishId { get; set; }
        public string Name { get; set; }
        public double Price  { get; set; }
        public double TotalPrice { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }
    }
}
