using BarangayInformationManagement.Models.Domains;
using BarangayInformationManagement.Repositories.Interface;
using BarangayInformationManagement.Utilities;
using MongoDB.Driver;

namespace BarangayInformationManagement.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IMongoCollection<AdminModel> admins;

        public AdminRepository(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            admins = database.GetCollection<AdminModel>(settings.AdminsCollectionName);
        }

        public async Task<AdminModel> GetByEmail(string email)
        {
            return await admins.Find(a => a.email == email).FirstOrDefaultAsync();
        }

        public async Task Insert(AdminModel admin)
        {
            await admins.InsertOneAsync(admin);
        }
    }
}
