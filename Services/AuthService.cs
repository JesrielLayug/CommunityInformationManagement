using BarangayInformationManagement.Models.Domains;
using BarangayInformationManagement.Models.Dtos;
using BarangayInformationManagement.Models;
using BarangayInformationManagement.Repositories.Interface;
using BarangayInformationManagement.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace BarangayInformationManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository adminRepository;
        private readonly IConfiguration configuration;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthService(
            IAdminRepository adminRepository,
            IConfiguration configuration,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider
            )
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.adminRepository = adminRepository;
            this.configuration = configuration;
            this.localStorage = localStorage;
        }

        public async Task<ResponseModel> Register(AdminInfoModel request)
        {
            try
            {
                CreatePasswordHash(request.password, out byte[] hash, out byte[] salt);

                var admin = new AdminModel
                {
                    role = "Admin",
                    name = request.name,
                    email = request.email,
                    passwordhash = hash,
                    passwordsalt = salt
                };

                await adminRepository.Insert(admin);
                return new ResponseModel
                {
                    isSuccess = true,
                    successMessage = "Successfully Register the Admin"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    isError = true,
                    errorMessage = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> Login(AdminLoginModel request)
        {
            try
            {
                var existingAdmin = await adminRepository.GetByEmail(request.email);
                if (existingAdmin == null)
                    return new ResponseModel
                    {
                        isError = true,
                        errorMessage = "Admin does not exist."
                    };

                var isPasswordCorrect = VerifyPasswordHash
                    (request.password, existingAdmin.passwordhash, existingAdmin.passwordsalt);

                if (isPasswordCorrect)
                {
                    var token = CreateToken(existingAdmin);
                    if(token != null)
                    {
                        await localStorage.SetItemAsync("token", token);

                        var state = await authenticationStateProvider.GetAuthenticationStateAsync();

                        if (state != null)
                        {
                            return new ResponseModel
                            {
                                isSuccess = true,
                                successMessage = "Successfully logged in user"
                            };
                        }
                    }
                }

                return new ResponseModel
                {
                    isWarning = true,
                    warningMessage = "Incorrect password"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    isError = true,
                    errorMessage = ex.Message,
                };
            }
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("token");
            await authenticationStateProvider.GetAuthenticationStateAsync();
        }

        #region Utilities

        private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hash);
            }
        }

        public void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        public string CreateToken(AdminModel admin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, admin.role),
                new Claim(ClaimTypes.Name, admin.name),
                new Claim(ClaimTypes.Email, admin.email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        #endregion

    }
}
