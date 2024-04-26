using BarangayInformationManagement.Models.Domains;

namespace BarangayInformationManagement.Repositories.Interface
{
    public interface IAdminRepository
    {
        Task Insert(AdminModel admin);
        Task<AdminModel> GetByEmail(string email);
    }
}
