using BarangayInformationManagement.Models.Dtos;
using BarangayInformationManagement.Models;

namespace BarangayInformationManagement.Services.Interface
{
    public interface IAuthService
    {
        Task<ResponseModel> Register(AdminInfoModel request);
        Task<ResponseModel> Login(AdminLoginModel request);
        Task Logout();
    }
}
