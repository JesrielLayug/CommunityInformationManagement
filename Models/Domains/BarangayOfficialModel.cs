using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BarangayInformationManagement.Models.Domains
{
    public class BarangayOfficialModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string fullname { get; set; }
        public string chairmanship { get; set; }
        public string position { get; set; }
        public string status { get; set; }
        public DateTime term_start { get; set; }
        public DateTime term_end { get; set; }
    }
}
