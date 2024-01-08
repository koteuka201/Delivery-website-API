using DeliveryAppBack;
using DeliveryAppBack.Middleware;
using DeliveryAppBack.Models.Address;
using DeliveryAppBack.Models.User;
using DeliveryAppBack.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//db
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AccountsDB>(options => options.UseSqlServer(connection));
var secondConnection = builder.Configuration.GetConnectionString("GarConnection");
builder.Services.AddDbContext<GarContext>(option => option.UseNpgsql(secondConnection));
//add services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IBasketService,BasketService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IAddressService,AddressService>();

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));

var secretKey = builder.Configuration.GetSection("Jwt:Key").Value;
var issuer = builder.Configuration.GetSection("Jwt:Issuer").Value;
var audience = builder.Configuration.GetSection("Jwt:Audience").Value;
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        IssuerSigningKey = signingKey,
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
