using DeliveryAppBack.Models.Busket;
using DeliveryAppBack.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace DeliveryAppBack.Services
{
    public class BasketService: IBasketService
    {
        private readonly AccountsDB _accountsDB;
        public BasketService(AccountsDB accountsDB)
        {
            _accountsDB = accountsDB;
        }
        public async Task<List<BasketModel>> GetUserCart(string token)
        {

            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            var basket = _accountsDB.UserBasket.Where(item => item.UserId == userId).Select(dish => new BasketModel
            {
                DishId = dish.DishId,
                Name = dish.Name,
                Price = dish.Price,
                TotalPrice = dish.TotalPrice,
                Amount = dish.Amount,
                Image = dish.Image,
            }).ToList();
            return basket;
        }
        public async Task AddDish(string token, Guid dishId)
        {
            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            var dish=await _accountsDB.DishMenu.FirstOrDefaultAsync(dish=>dish.Id==dishId);
            var incart=await _accountsDB.UserBasket.FirstOrDefaultAsync(id=>id.UserId== userId && id.DishId==dishId);

            if (incart != null)
            {
                incart.Amount += 1;
                incart.TotalPrice= incart.Price*incart.Amount;

                await _accountsDB.SaveChangesAsync();
            }
            else
            {
                DishBasketDTO dishBasketDTO = new DishBasketDTO
                {
                    UserId = userId,
                    DishId = dishId,
                    Name = dish.Name,
                    Price = dish.Price,
                    Amount = 1,
                    Image = dish.Image,
                };
                dishBasketDTO.TotalPrice= dishBasketDTO.Amount * dishBasketDTO.Price;
                await _accountsDB.UserBasket.AddAsync(dishBasketDTO);
                await _accountsDB.SaveChangesAsync();
            }
        }
        public async Task DeleteDish(string token, Guid dishId, bool increase)
        {
            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            var incart = await _accountsDB.UserBasket.FirstOrDefaultAsync(id => id.UserId == userId && id.DishId == dishId);
            if(incart != null)
            {
                if (increase)
                {
                    incart.Amount -= 1;
                    if (incart.Amount!=0) 
                    {
                        incart.TotalPrice = incart.Price * incart.Amount;
                    }
                    else
                    {
                        _accountsDB.UserBasket.Remove(incart);
                    }
                    await _accountsDB.SaveChangesAsync();
                }
                else
                {
                    _accountsDB.UserBasket.Remove(incart);
                    await _accountsDB.SaveChangesAsync();
                }
            }
        }
    }

}
