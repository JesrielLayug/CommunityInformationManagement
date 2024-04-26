using System.ComponentModel.DataAnnotations;

namespace BarangayInformationManagement.Models.Dtos
{
    public class BarangayOfficialInfoModel
    {
        public string id { get; set; }
        [Required]
        public string fullname { get; set; }
        [Required]
        public string chairmanship { get; set; }
        [Required]
        public string position { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public DateTime term_start { get; set; }
        [Required]
        public DateTime term_end { get; set;}
    }
}
