using EShop.Core.Common.Enums;

namespace EShop.Core.Domain
{
    public interface IFileService
    {
        Task<long> UploadFileAsync(byte[] fileBytes, string originalName, string fileDescription, FileType? type = null);
        Task<Tuple<long, long>> UploadBigImageAndResizeAsync(byte[] fileBytes, string originalName, string fileDescription);
        Task<(byte[], string)> DownloadFileAsync(long fileId);
        Task<byte[]> CreatePdfPricingFileForCategoryAsync(long categoryId);
    }
}