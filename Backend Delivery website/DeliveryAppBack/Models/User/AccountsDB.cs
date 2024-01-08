using DeliveryAppBack.Models.Busket;
using DeliveryAppBack.Models.Dish;
using DeliveryAppBack.Models.Order;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeliveryAppBack.Models.User
{
    public class AccountsDB : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<TokenModel> LogOutTokens { get; set; }
        public DbSet<DishDTO> DishMenu { get; set; }
        public DbSet<RatingDish> UserRate { get; set; }
        public DbSet<DishBasketDTO> UserBasket { get; set; }
        public DbSet<OrderDTO> UserOrders { get; set; }
        public DbSet<TempHistory> TempOrderhistory { get; set; }
        public AccountsDB(DbContextOptions<AccountsDB> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenModel>().HasKey(t => t.Token);
            modelBuilder.Entity<RatingDish>().HasKey(r => new { r.DishId, r.UserId });
            modelBuilder.Entity<TempHistory>().HasKey(e => e.Id);
        }

    }
}
