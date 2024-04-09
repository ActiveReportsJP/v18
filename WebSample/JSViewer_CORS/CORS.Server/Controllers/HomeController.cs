using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CORS.Server.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        [Route("index")]
        public ActionResult Index()
        {
            return Resource("index.html");
        }

        [HttpGet]
        [Route("{file}")]
        public ActionResult Resource(string file)
        {
            string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "wwwroot", file);
            if (!System.IO.File.Exists(filePath))
                return new HttpNotFoundResult();

            if (Path.GetExtension(file) == ".html")
                return new ContentResult() { Content = System.IO.File.ReadAllText(filePath), ContentType = "text/html" };

            var resFile = System.IO.File.ReadAllBytes(filePath);

            if (Path.GetExtension(file) == ".ico")
                return new FileContentResult(resFile, "image/x-icon") { FileDownloadName = file };

            return new FileContentResult(resFile, GetMimeType(file)) { FileDownloadName = file };
        }

        [HttpGet]
        [Route("reports")]
        public ActionResult Reports()
        {
            string[] validExtensions = {".rdl", ".rdlx", ".rdlx-master", ".rpx"};
            var reportsList = GetFileStoreReports(validExtensions);
            return new JsonResult
            {
                Data = reportsList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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

        /// <summary>
        /// Gets report names from folder
        /// </summary>
        /// <returns>Report names</returns>
        private string[] GetFileStoreReports(string[] validExtensions)
        {
            return Startup.ReportsDirectory
                .EnumerateFiles("*.*")
                .Select(x => x.Name)
                .Where(x => validExtensions.Any(x.EndsWith))
                .ToArray();
        }
    }
}