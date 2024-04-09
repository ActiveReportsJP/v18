using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace JsViewerBatchPrint_MVC_Core.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public object Index() => Resource("index.html");

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{file}")]
        public object Resource(string file)
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, file);

            if (!System.IO.File.Exists(filePath))
                return new NotFoundResult();

            if (Path.GetExtension(file) == ".html")
                return new ContentResult() { Content = System.IO.File.ReadAllText(filePath), ContentType = "text/html" };

            var resFile = System.IO.File.ReadAllBytes(filePath);

            if (Path.GetExtension(file) == ".ico")
                return new FileContentResult(resFile, "image/x-icon") { FileDownloadName = file };

            return new FileContentResult(resFile, GetMimeType(file)) { FileDownloadName = file };
        }


        /// <summary>
        /// Gets the MIME type from the file extension
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>MIME type</returns>
        private static string GetMimeType(string fileName)
        {
            if (fileName.EndsWith(".css"))
                return "text/css";

            if (fileName.EndsWith(".js"))
                return "text/javascript";

            return "text/html";
        }
    }
}