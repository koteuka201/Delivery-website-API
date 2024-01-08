using DeliveryAppBack.Enums;
using DeliveryAppBack.Models.Dish;
using System;

namespace DeliveryAppBack.Services
{
    public interface IDishService
    {
        public Task<DishPagedListDto> GetDishMenu(List<DishCategory> category, bool vegeterian, DishSorting sorting, int page);
        public Task<DishDTO> GetDescription(Guid id);
        public Task<bool> CheckAbleToRate(Guid id, string token);
        public Task SetRatingToDish(Guid dishId, string token, double rating);
    }
}
