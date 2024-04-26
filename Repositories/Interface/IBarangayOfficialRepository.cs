using BarangayInformationManagement.Models.Domains;

namespace BarangayInformationManagement.Repositories.Interface
{
    public interface IBarangayOfficialRepository
    {
        Task Insert(BarangayOfficialModel barangayOfficialModel);
        Task Update(string id, BarangayOfficialModel barangayOfficial);
        Task Delete(string id);
        Task<BarangayOfficialModel> Get(string id);
        Task<IEnumerable<BarangayOfficialModel>> GetAll();
    }
}
