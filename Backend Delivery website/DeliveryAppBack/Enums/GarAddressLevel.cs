using System.Text.Json.Serialization;

namespace DeliveryAppBack.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GarAddressLevel
    {
        hi,
        Region, 
        AdministrativeArea, 
        MunicipalArea, 
        RuralUrbanSettlement, 
        City, 
        Locality, 
        ElementOfPlanningStructure, 
        ElementOfRoadNetwork, 
        Land, 
        Building, 
        Room, 
        RoomInRooms, 
        AutonomousRegionLevel, 
        IntracityLevel, 
        AdditionalTerritoriesLevel, 
        LevelOfObjectsInAdditionalTerritories, 
        CarPlace,
        Ownership,
        House,
        Household,
        Garage,
        Building_,
        Mine,
        Structure,
        Facility,
        Letter,
        Corpus,
        Basement,
        BoilerRoom,
        Cellar,
        ConstructionInProgress
    }
}
