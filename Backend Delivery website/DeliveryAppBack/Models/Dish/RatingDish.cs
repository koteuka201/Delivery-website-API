namespace DeliveryAppBack.Models.Dish
{
    public class RatingDish
    {
        public Guid DishId { get; set; }
        public string UserId { get; set; }
        public double Rating { get; set; }
    }
}
