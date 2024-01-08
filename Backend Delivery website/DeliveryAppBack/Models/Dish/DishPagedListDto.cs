namespace DeliveryAppBack.Models.Dish
{
    public class PageInfoModule
    {
        public int size { get; set; }
        public int count { get; set; }
        public int current { get; set; }
    }
    public class DishPagedListDto
    {   
        public List<DishDTO> Dishes { get; set; }
        public PageInfoModule pagination { get; set; }
        
    }
}
