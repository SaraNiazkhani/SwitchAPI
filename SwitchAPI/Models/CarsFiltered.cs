using MongoDB.Bson;

namespace SwitchAPI.Models
{
    public class CarsFiltered
    {
        [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(BsonType.ObjectId)]

        public string _id { get; set; }
        public string brandname { get; set; }
        public string modelname { get; set; }
        public int year { get; set; }
        public int usagekilometere { get; set; }
        public string gearboxtype { get; set; }
        public string bodystatus { get; set; }
        public double price { get; set; }
        public string fueltype { get; set; }
    }
}
