using DeliveryAppBack.Enums;
using DeliveryAppBack.Models.Dish;
using DeliveryAppBack.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;

namespace DeliveryAppBack.Services
{
    public class DishService : IDishService
    {
        private readonly AccountsDB _accountDB;
        public DishService(AccountsDB accountDB)
        {
            _accountDB = accountDB;
        }
        public async Task<DishPagedListDto> GetDishMenu(List<DishCategory> category, bool vegeterian, DishSorting sorting, int page)
        {
            var allDishes=new List<DishDTO>();
            foreach(var item in  category)
            {
                var dishes = _accountDB.DishMenu.Where(dish => (item == dish.Category) && (vegeterian == dish.Vegeterian)).ToList();
                allDishes.AddRange(dishes);
            }
            switch (sorting.ToString())
            {
                case "NameAsc":
                    allDishes = allDishes.OrderBy(d => d.Name).ToList();
                    break;
                case "NameDesc":
                    allDishes = allDishes.OrderByDescending(d => d.Name).ToList();
                    break;
                case "PriceAsc":
                    allDishes = allDishes.OrderBy(d => d.Price).ToList();
                    break;
                case "PriceDesc":
                    allDishes = allDishes.OrderByDescending(d => d.Price).ToList();
                    break;
                case "RatingAsc":
                    allDishes = allDishes.OrderBy(d => d.Rating).ToList();
                    break;
                case "RatingDesc":
                    allDishes = allDishes.OrderByDescending(d => d.Rating).ToList();
                    break;
            }
            int pages = 5;
            var skip = (page - 1) * pages;
            var PaginDish = allDishes.Skip(skip).Take(pages).ToList();
            DishPagedListDto dishPagedListDto = new DishPagedListDto
            {
                Dishes=PaginDish,
                pagination=new PageInfoModule
                {
                    size=pages,
                    count= allDishes.Count(),
                    current=page
                }
            };

            return dishPagedListDto;
        }
        public async Task<DishDTO> GetDescription(Guid id)
        {
            var dish = await _accountDB.DishMenu.FirstOrDefaultAsync(d => d.Id == id);
            return dish;
        }
        public async Task<bool> CheckAbleToRate(Guid dishId, string token)
        {
            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            var checkRate= _accountDB.TempOrderhistory.FirstOrDefault(ord => ((ord.UserId == userId) && (ord.DishId == dishId)));
            if (checkRate == null)
            {
                return true;
            }
            return false;
        }
        public async Task SetRatingToDish(Guid dishId, string token, double rating)
        {
            var checkAble = CheckAbleToRate(dishId, token);
            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            if (checkAble.Result == true)
            {
                var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
                var dish= await _accountDB.DishMenu.FirstOrDefaultAsync(d=> d.Id == dishId);
                RatingDish ratingDish = new RatingDish
                {
                    DishId= dishId,
                    UserId= userId,
                    Rating= rating,
                };
                await _accountDB.UserRate.AddAsync(ratingDish);
                await _accountDB.SaveChangesAsync();
                var sumRate = _accountDB.UserRate.Where(d => d.DishId == dishId).Sum(u => u.Rating);
                var countRate = _accountDB.UserRate.Count(d => d.DishId == dishId);
                double avgRate = (double)(sumRate / countRate);
                dish.Rating = avgRate;
                await _accountDB.SaveChangesAsync();
            }

        }
    }
}
