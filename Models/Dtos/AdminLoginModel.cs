using System.ComponentModel.DataAnnotations;

namespace BarangayInformationManagement.Models.Dtos
{
    public class AdminLoginModel
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
