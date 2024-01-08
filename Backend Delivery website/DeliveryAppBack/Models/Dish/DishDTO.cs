using DeliveryAppBack.Enums;

namespace DeliveryAppBack.Models.Dish
{
    public class DishDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public bool Vegeterian { get; set; }
        public double Rating { get; set; }
        public DishCategory Category { get; set; }
    }
}
