using DeliveryAppBack.Enums;
using DeliveryAppBack.Models.Address;
using DeliveryAppBack.Models.Busket;
using DeliveryAppBack.Models.Order;
using DeliveryAppBack.Models.User;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace DeliveryAppBack.Services
{
    public class OrderService: IOrderService
    {
        private readonly AccountsDB _accountsDB;
        private readonly GarContext _garContext;
        public OrderService(AccountsDB accountsDB,GarContext garContext)
        {
            _accountsDB = accountsDB;
            _garContext = garContext;
        }
        public async Task<OrderCreateDTO> CreateOrder(string token)
        {
            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            var user = _accountsDB.Users.FirstOrDefault(user => user.Id.ToString() == userId);
            Guid orderId = Guid.NewGuid();
            var checkBasket = _accountsDB.UserBasket.FirstOrDefault(id => id.UserId == userId);
            if (checkBasket != null)
            {
                var dishesInbasket = _accountsDB.UserBasket.Where(id => id.UserId == userId).Select(dish => new TempHistory
                {
                    DishId = dish.DishId,
                    OrderId = orderId,
                    UserId = userId,
                    Amount = dish.Amount,
                }).ToList();
                double price = 0;
                if (dishesInbasket.Any())
                {
                    foreach (var tempHistory in dishesInbasket)
                    {
                        var dish = await _accountsDB.DishMenu.FirstOrDefaultAsync(d => d.Id == tempHistory.DishId);
                        if (dish != null)
                        {
                            price += dish.Price * tempHistory.Amount;
                        }
                    }
                    await _accountsDB.TempOrderhistory.AddRangeAsync(dishesInbasket);
                    await _accountsDB.SaveChangesAsync();
                }
                var addressObject = _garContext.AsAddrObjs.FirstOrDefault(obj => obj.Objectguid.ToString() == user.AdressId);
                var address = addressObject.Typename + " " + addressObject.Name;
                OrderDTO orderDTO = new OrderDTO
                {
                    Id = orderId,
                    UserId = userId,
                    DeliveryTime = DateTime.Now,
                    OrderTime = DateTime.Now,
                    Status = OrderStatusEnum.InProcess,
                    Price = (int)price,
                    Address = address
                };
                OrderCreateDTO orderCreateDTO = new OrderCreateDTO
                {
                    DeliveryTime = orderDTO.DeliveryTime,
                    AdressId = user.AdressId
                };
                var basketToDelete= _accountsDB.UserBasket.Where(item=>item.UserId == userId).ToList();
                _accountsDB.UserBasket.RemoveRange(basketToDelete);
                await _accountsDB.UserOrders.AddRangeAsync(orderDTO);
                await _accountsDB.SaveChangesAsync();
                return orderCreateDTO;
            }
            return null;
        }
        public async Task ConfirmOrder(string token, Guid orderId)
        {
            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            var order = await _accountsDB.UserOrders.FirstOrDefaultAsync(order => order.Id == orderId);
            if (order != null && order.UserId==userId) 
            {
                order.Status = OrderStatusEnum.Delivered;
                await _accountsDB.SaveChangesAsync();
            }
        }
        public async Task<OrderModel> GetInfoOrder(string token, Guid orderId)
        {
            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            var order = _accountsDB.UserOrders.FirstOrDefault(or => or.Id == orderId);
            var ordersTemp= _accountsDB.TempOrderhistory.Where(ord=>ord.OrderId == orderId).Select(item=> new TempHistory
            {
                Id=item.Id,
                DishId=item.DishId,
                OrderId=orderId,
                UserId=userId,
                Amount=item.Amount

            }).ToList();
            if(order != null && order.UserId == userId)
            {
                var dishes=new List<BasketModel>();
                foreach(var item in  ordersTemp)
                {
                    var dish = _accountsDB.DishMenu.FirstOrDefault(dish => dish.Id == item.DishId);
                    BasketModel basketModel = new BasketModel
                    {
                        DishId=dish.Id,
                        Name=dish.Name,
                        Price=dish.Price,
                        TotalPrice=dish.Price*item.Amount,
                        Amount=item.Amount,
                        Image=dish.Image
                    };
                    dishes.Add(basketModel);
                }
                OrderModel orderModel = new OrderModel
                {
                    Id = order.Id,
                    UserId = userId,
                    DeliveryTime = order.DeliveryTime,
                    OrderTime = order.OrderTime,
                    Status = order.Status,
                    Price = order.Price,
                    Dishes = dishes,
                    Address=order.Address,
                };
                return orderModel;
            }  
            return null;
        }
        public async Task<List<OrderInfoDTO>> GetListOfOrders(string token)
        {
            var JWT = new JwtSecurityTokenHandler();
            var Token = JWT.ReadToken(token) as JwtSecurityToken;
            var userId = Token.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            var orders = _accountsDB.UserOrders.Where(or => or.UserId == userId).Select(item=> new OrderInfoDTO
            {
                Id=item.Id,
                DeliveryTime=item.DeliveryTime,
                OrderTime=item.OrderTime,
                Status=item.Status,
                Price=item.Price,
            }).ToList();
            return orders;
        }
    }
}
