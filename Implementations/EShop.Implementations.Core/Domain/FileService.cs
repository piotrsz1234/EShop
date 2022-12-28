using EShop.Core.Domain;
using EShop.Core.Entities.Enums;
using EShop.Core.Infrastructure.Repositories;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using File = EShop.Core.Entities.File;

namespace EShop.Implementations.Core.Domain
{
    internal sealed class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
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
            };
            
            await _fileRepository.AddAsync(file);

            await _fileRepository.SaveChangesAsync();

            return file.Id;
        }

        public async Task<Tuple<long, long>> UploadBigImageAndResizeAsync(byte[] fileBytes, string originalName, string fileDescription)
        {
            var bigImageId = await UploadFileAsync(fileBytes, originalName, fileDescription);
            using (var stream = new MemoryStream(fileBytes)) {
                using (var image = Image.FromStream(stream)) {
                    Bitmap output = null;
                    if (image.Width >= image.Height) {
                        output = new Bitmap(image, MiniatureImageMaxSize, image.Height * (MiniatureImageMaxSize / image.Width));
                    } else {
                        output = new Bitmap(image, image.Width * (MiniatureImageMaxSize / image.Height), MiniatureImageMaxSize);
                    }

                    using (var outputStream = new MemoryStream()) {
                        output.Save(outputStream, image.RawFormat);
                        var smallImageId =  await UploadFileAsync(outputStream.ToArray(), originalName, fileDescription, FileType.SmallImage);

                        return new Tuple<long, long>(bigImageId, smallImageId);
                    }
                }
            }
        }
    }
}