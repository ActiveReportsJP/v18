using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace JSViewerReactCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public HomeController(IWebHostEnvironment env) => _env = env;

        public IActionResult Index()
        {
            return new PhysicalFileResult(Path.Combine(_env.WebRootPath, "index.html"), "text/html");
        }

        [HttpGet("reports")]
        public ActionResult Reports()
        {
            string[] validExtensions = {".rdl", ".rdlx", ".rdlx-master", ".rpx"};

            var reportsList = GetFileStoreReports(validExtensions);
            return new ObjectResult(reportsList);
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