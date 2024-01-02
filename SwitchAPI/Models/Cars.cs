using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SwitchAPI.Models
{
    public class Cars
    {
        [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(BsonType.ObjectId)]

        public string _id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string uniquename { get; set; }
        public string createdon { get; set; }
        public string createdby { get; set; }
        public string modifiedon { get; set; }
        public string modifiedby { get; set; }
        public int statecode { get; set; }
    }
}
