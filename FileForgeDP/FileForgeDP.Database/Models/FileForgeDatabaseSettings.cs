namespace FileForgeDP.Database.Models
{
    public class FileForgeDatabaseSettings : IFileForgeDatabaseSettings
    {
        public string CollectionName { get; set; }

        public string WorkspaceCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }

    public interface IFileForgeDatabaseSettings
    {
        string CollectionName { get; set; }

        string WorkspaceCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
