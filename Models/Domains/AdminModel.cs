using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BarangayInformationManagement.Models.Domains
{
    public class AdminModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string role { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public byte[] passwordhash { get; set; }
        public byte[] passwordsalt { get; set; }
    }
}
