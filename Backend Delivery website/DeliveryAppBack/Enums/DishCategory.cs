using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeliveryAppBack.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum DishCategory
    {
        Wok,
        Pizza,
        Soup,
        Dessert,
        Drink
    }
}
