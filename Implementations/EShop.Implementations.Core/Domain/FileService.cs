using EShop.Core.Domain;
using EShop.Core.Infrastructure.Repositories;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Configuration;
using System.Drawing;
using EShop.Core.Common.Enums;
using File = EShop.Core.Entities.File;
using Image = System.Drawing.Image;
using Path = System.IO.Path;

namespace EShop.Implementations.Core.Domain
{
    internal sealed class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public FileService(IFileRepository fileRepository, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _fileRepository = fileRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        private string FilePath => ConfigurationManager.AppSettings["FileFolder"];
        private int MiniatureImageMaxSize => int.Parse(ConfigurationManager.AppSettings["MiniatureImageMaxSize"]);

        public async Task<long> UploadFileAsync(byte[] fileBytes, string originalName, string fileDescription, FileType? type = null)
        {
            var extension = Path.GetExtension(originalName);

            var fileType = type ?? FileType.Other;
            
            if (fileType == FileType.Other && MimeTypes.TryGetMimeType(originalName, out var typing)) {
                if (typing.ToLower().Contains("image")) fileType = FileType.OtherImage;
            }

            var discName = $"{Guid.NewGuid()}.{extension}";
            System.IO.File.WriteAllBytes(Path.Combine(FilePath, discName), fileBytes);

            var file = new File() {
                Description = fileDescription,
                DiscName = discName,
                Type = fileType,
                DisplayFileName = originalName,
                InsertDateUtc = DateTime.UtcNow,
                ModificationDateUtc = DateTime.UtcNow,
            };
            
            await _fileRepository.AddAsync(file);

            await _fileRepository.SaveChangesAsync();

            return file.Id;
        }

        public async Task<Tuple<long, long>> UploadBigImageAndResizeAsync(byte[] fileBytes, string originalName, string fileDescription)
        {
            var bigImageId = await UploadFileAsync(fileBytes, originalName, fileDescription, FileType.MainImage);
            using (var stream = new MemoryStream(fileBytes)) {
                using (var image = Image.FromStream(stream)) {
                    Bitmap output;
                    output = image.Width >= image.Height
                        ? new Bitmap(image, MiniatureImageMaxSize, image.Height * (MiniatureImageMaxSize / image.Width))
                        : new Bitmap(image, image.Width * (MiniatureImageMaxSize / image.Height), MiniatureImageMaxSize);

                    using (var outputStream = new MemoryStream()) {
                        output.Save(outputStream, image.RawFormat);
                        var smallImageId =  await UploadFileAsync(outputStream.ToArray(), originalName, fileDescription, FileType.SmallImage);

                        return new Tuple<long, long>(bigImageId, smallImageId);
                    }
                }
            }
        }

        public async Task<(byte[], string)> DownloadFileAsync(long fileId)
        {
            var file = await _fileRepository.GetOneAsync(fileId);

            if (file is null) return default;

            return new (System.IO.File.ReadAllBytes(Path.Combine(FilePath, file.DiscName)), file.DisplayFileName);
        }

        public async Task<byte[]> CreatePdfPricingFileForCategoryAsync(long categoryId)
        {
            var categoryIds = await _categoryRepository.GetAllWithDependentAsync(categoryId);

            var products = await _productRepository.GetAllAsync(x => x.IsDeleted == false && categoryIds.Contains(x.Id),
                 x => x.Category);

            using var stream = new MemoryStream();

            using (var writer = new PdfWriter(stream)) {
                using (var pdfDocument = new PdfDocument(writer)) {
                    var document = new Document(pdfDocument);
                    var table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();

                    table.AddCell("Category");
                    table.AddCell("Name");
                    table.AddCell("Price");
                    
                    foreach (var product in products) {
                        table.AddCell(product.Category.Name);
                        table.AddCell(product.Name);
                        table.AddCell(product.Price.ToString("F"));
                    }

                    document.Add(table);
                    document.Close();
                }
            }

            return stream.ToArray();
        }
    }
}