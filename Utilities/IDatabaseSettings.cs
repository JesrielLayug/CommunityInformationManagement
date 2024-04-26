namespace BarangayInformationManagement.Utilities
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string AdminsCollectionName { get; set; }
        string BarangayOfficialCollectionName {  get; set; }
    }
}
