namespace BarangayInformationManagement.Models
{
    public class ResponseModel
    {
        public bool isSuccess { get; set; }
        public bool isWarning { get; set; }
        public bool isError { get; set; }
        public string successMessage { get; set; }
        public string errorMessage { get; set; }
        public string warningMessage { get; set; }
    }
}
