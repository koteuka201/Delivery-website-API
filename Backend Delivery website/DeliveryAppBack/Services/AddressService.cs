using DeliveryAppBack.Enums;
using DeliveryAppBack.Models.Address;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAppBack.Services
{
    public class AddressService: IAddressService
    {
        private readonly GarContext _garcontext;
        public AddressService(GarContext garcontext)
        {
            _garcontext = garcontext;
        }
        public async Task<List<SearchAddressModel>> GetAddress(long parentObjectId, string? query)
        {
            var objectsId=_garcontext.AsAdmHierarchies.Where(parId=> parId.Parentobjid==parentObjectId).ToList();
            var objects=new List<SearchAddressModel>();
            foreach(var item in objectsId)
            {
                var Object =await _garcontext.AsAddrObjs.FirstOrDefaultAsync(obj=> obj.Objectid==item.Objectid);
                SearchAddressModel searchAddressModel = new SearchAddressModel
                {
                    objectId = Object.Objectid,
                    objectGuid = Object.Objectguid,
                    text = Object.Typename + " " + Object.Name,
                    objectLevel = ((GarAddressLevel)int.Parse(Object.Level)),
                    objectLevelText = ((GarAddressLevelText)int.Parse(Object.Level)).ToString()
                };
                if (query != null)
                {
                    if (searchAddressModel.text.Contains(query))
                    {
                        objects.Add(searchAddressModel);
                    }
                }
                else
                {
                    objects.Add(searchAddressModel);
                }
            }
            return objects;
        }
        public async Task<List<SearchAddressModel>> GetChain(Guid ObjectId)
        {
            var chain=new List<SearchAddressModel>();
            var startObjAddress=_garcontext.AsAddrObjs.FirstOrDefault(obj=>obj.Objectguid== ObjectId);
            if (startObjAddress == null)
            {
                var startObjHouse = _garcontext.AsHouses.FirstOrDefault(obj => obj.Objectguid == ObjectId);
                SearchAddressModel searchAddressModelHouse = new SearchAddressModel
                {
                    objectId = startObjHouse.Objectid,
                    objectGuid = startObjHouse.Objectguid,
                    text = startObjHouse.Housenum,
                    objectLevel = (GarAddressLevel)int.Parse((startObjHouse.Housetype + 17).ToString()),
                    objectLevelText = ((GarHouseType)int.Parse((startObjHouse.Housetype).ToString())).ToString()
                };
                chain.Add(searchAddressModelHouse);
                var parentObjHouse= _garcontext.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == startObjHouse.Objectid);
                if (parentObjHouse != null)
                {
                    var tempObjHouse = _garcontext.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == parentObjHouse.Parentobjid);
                    while (tempObjHouse != null)
                    {
                        var listObj = _garcontext.AsAddrObjs.FirstOrDefault(obj => obj.Objectid == tempObjHouse.Objectid);
                        SearchAddressModel searchAddressModel1 = new SearchAddressModel//
                        {
                            objectId = listObj.Objectid,
                            objectGuid = listObj.Objectguid,
                            text = listObj.Typename + " " + listObj.Name,
                            objectLevel = ((GarAddressLevel)int.Parse(listObj.Level)),
                            objectLevelText = ((GarAddressLevelText)int.Parse(listObj.Level)).ToString()
                        };
                        chain.Add(searchAddressModel1);
                        var tempObjHouse2= _garcontext.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == listObj.Objectid);
                        tempObjHouse = _garcontext.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == tempObjHouse2.Parentobjid);
                    }
                    chain.Reverse();
                    return chain;
                }
                return chain;
            }
            SearchAddressModel searchAddressModel = new SearchAddressModel
            {
                objectId = startObjAddress.Objectid,
                objectGuid = startObjAddress.Objectguid,
                text = startObjAddress.Typename + " " + startObjAddress.Name,
                objectLevel = ((GarAddressLevel)int.Parse(startObjAddress.Level)),
                objectLevelText = ((GarAddressLevelText)int.Parse(startObjAddress.Level)).ToString()
            };
            chain.Add(searchAddressModel);
            var parentObjAddress = _garcontext.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == startObjAddress.Objectid);
            if(parentObjAddress != null)
            {
                var tempObjAddress = _garcontext.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == parentObjAddress.Parentobjid);
                while (tempObjAddress != null)
                {
                    var listObj = _garcontext.AsAddrObjs.FirstOrDefault(obj => obj.Objectid == tempObjAddress.Objectid);
                    SearchAddressModel searchAddressModel1 = new SearchAddressModel
                    {
                        objectId = listObj.Objectid,
                        objectGuid = listObj.Objectguid,
                        text = listObj.Typename + " " + listObj.Name,
                        objectLevel = ((GarAddressLevel)int.Parse(listObj.Level)),
                        objectLevelText = ((GarAddressLevelText)int.Parse(listObj.Level)).ToString()
                    };
                    chain.Add(searchAddressModel1);
                    var tempObjAddress2= _garcontext.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == listObj.Objectid);
                    tempObjAddress= _garcontext.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == tempObjAddress2.Parentobjid);
                }
                chain.Reverse();
                return chain;
            }
            return chain;
        }
    }
}
