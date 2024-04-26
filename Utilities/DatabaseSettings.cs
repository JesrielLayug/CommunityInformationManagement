namespace BarangayInformationManagement.Utilities
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AdminsCollectionName { get; set; }
        public string BarangayOfficialCollectionName {  get; set; }
    }
}
