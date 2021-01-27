namespace FileForgeDP.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Threading.Tasks;

    public static class BytesExtensions
    {
        public static async Task<byte[]> ToBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static IFormFile ToIFormFIle(this byte[] bytes, string fileName)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return new FormFile(memoryStream, 0, bytes.Length, fileName, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };
            }
        }
    }
}
