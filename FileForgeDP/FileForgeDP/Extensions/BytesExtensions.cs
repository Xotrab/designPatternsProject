namespace FileForgeDP.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="BytesExtensions" />.
    /// </summary>
    public static class BytesExtensions
    {
        /// <summary>
        /// The ToBytes.
        /// </summary>
        /// <param name="formFile">The formFile<see cref="IFormFile"/>.</param>
        /// <returns>The <see cref="Task{byte[]}"/>.</returns>
        public static async Task<byte[]> ToBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// The ToIFormFIle.
        /// </summary>
        /// <param name="bytes">The bytes<see cref="byte[]"/>.</param>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="IFormFile"/>.</returns>
        public static IFormFile ToIFormFIle(this byte[] bytes, string fileName)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return new FormFile(memoryStream, 0, bytes.Length, fileName, fileName);
            }
        }
    }
}
