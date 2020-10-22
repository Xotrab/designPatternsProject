namespace FileForgeDP.Database.Models
{
    /// <summary>
    /// Defines the <see cref="FileForgeDatabaseSettings" />.
    /// </summary>
    public class FileForgeDatabaseSettings : IFileForgeDatabaseSettings
    {
        /// <summary>
        /// Gets or sets the CollectionName.
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// Gets or sets the ConnectionString.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the DatabaseName.
        /// </summary>
        public string DatabaseName { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="IFileForgeDatabaseSettings" />.
    /// </summary>
    public interface IFileForgeDatabaseSettings
    {
        /// <summary>
        /// Gets or sets the CollectionName.
        /// </summary>
        string CollectionName { get; set; }

        /// <summary>
        /// Gets or sets the ConnectionString.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the DatabaseName.
        /// </summary>
        string DatabaseName { get; set; }
    }
}
