using BarangayInformationManagement.Models;
using BarangayInformationManagement.Models.Dtos;

namespace BarangayInformationManagement.Services.Interface
{
    public interface IBarangayOfficialService
    {
        Task<IEnumerable<BarangayOfficialInfoModel>> GetAll();
        Task<BarangayOfficialInfoModel> Get(string id);
        Task<ResponseModel> Insert(BarangayOfficialInfoModel model);
        Task<ResponseModel> Update(BarangayOfficialInfoModel model);
        Task<ResponseModel> Delete(string id);
    }
}
