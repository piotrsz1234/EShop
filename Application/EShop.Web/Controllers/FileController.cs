using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EShop.Core.Domain;

namespace EShop.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<ActionResult> DownloadFile(long fileId)
        {
            var result = await _fileService.DownloadFileAsync(fileId);

            return File(result.Item1, MimeMapping.GetMimeMapping(result.Item2), result.Item2);
        }
    }
}