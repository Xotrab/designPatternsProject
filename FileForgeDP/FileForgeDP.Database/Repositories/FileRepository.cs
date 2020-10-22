namespace FileForgeDP.Database.Services
{
    using FileForgeDP.Database.Models;
    using MongoDB.Driver;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FileRepository" />.
    /// </summary>
    public class FileRepository
    {
        /// <summary>
        /// Defines the mFiles.
        /// </summary>
        private readonly IMongoCollection<FileModel> mFiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings<see cref="IFileForgeDatabaseSettings"/>.</param>
        public FileRepository(IFileForgeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            mFiles = database.GetCollection<FileModel>(settings.CollectionName);
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="file">The file<see cref="FileModel"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string Create(FileModel file)
        {
            mFiles.InsertOne(file);

            return file.Id;
        }

        /// <summary>
        /// The Get.
        /// </summary>
        /// <returns>The <see cref="List{FileModel}"/>.</returns>
        public List<FileModel> Get() =>
            mFiles.Find(file => true).ToList();

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="Id">The Id<see cref="string"/>.</param>
        /// <returns>The <see cref="FileModel"/>.</returns>
        public FileModel Get(string Id) =>
            mFiles.Find(file => file.Id == Id).FirstOrDefault();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="id">The id<see cref="string"/>.</param>
        /// <param name="fileModel">The fileModel<see cref="FileModel"/>.</param>
        public void Update(string id, FileModel fileModel) =>
            mFiles.ReplaceOne(file => file.Id == id, fileModel);

        /// <summary>
        /// The Remove.
        /// </summary>
        /// <param name="modelIn">The modelIn<see cref="FileModel"/>.</param>
        public void Remove(FileModel modelIn) =>
            mFiles.DeleteOne(file => file.Id == modelIn.Id);

        /// <summary>
        /// The Remove.
        /// </summary>
        /// <param name="id">The id<see cref="string"/>.</param>
        public void Remove(string id) =>
            mFiles.DeleteOne(file => file.Id == id);
    }
}
