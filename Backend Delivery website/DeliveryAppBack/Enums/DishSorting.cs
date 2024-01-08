using System.Text.Json.Serialization;

namespace DeliveryAppBack.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DishSorting
    {
        NameAsc, NameDesc, PriceAsc, PriceDesc, RatingAsc, RatingDesc
    }
}
