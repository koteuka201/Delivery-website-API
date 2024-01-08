using DeliveryAppBack.Models.Address;

namespace DeliveryAppBack.Services
{
    public interface IAddressService
    {
        public Task<List<SearchAddressModel>> GetAddress(long parentObjectId, string? query);
        public Task<List<SearchAddressModel>> GetChain(Guid ObjectId);
    }
}
