using BarangayInformationManagement.Models.Domains;
using BarangayInformationManagement.Repositories.Interface;
using BarangayInformationManagement.Utilities;
using MongoDB.Driver;

namespace BarangayInformationManagement.Repositories
{
    public class BarangayOfficialRepository : IBarangayOfficialRepository
    {
        private readonly IMongoCollection<BarangayOfficialModel> barangayOfficials;

        public BarangayOfficialRepository(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            barangayOfficials = database.GetCollection<BarangayOfficialModel>(settings.BarangayOfficialCollectionName);
        }

        public async Task<BarangayOfficialModel> Get(string id)
        {
            return await barangayOfficials.Find(x => x.id == id).FirstOrDefaultAsync();
        }

        public async Task Insert(BarangayOfficialModel barangayOfficialModel)
        {
            await barangayOfficials.InsertOneAsync(barangayOfficialModel);
        }

        public async Task Delete(string id)
        {
            await barangayOfficials.DeleteOneAsync(x => x.id==id);
        }

        public async Task Update(string id, BarangayOfficialModel barangayOfficial)
        {
            await barangayOfficials.ReplaceOneAsync(x => x.id == id, barangayOfficial);
        }

        public async Task<IEnumerable<BarangayOfficialModel>> GetAll()
        {
            return await barangayOfficials.Find(x => true).ToListAsync();
        }
    }
}
