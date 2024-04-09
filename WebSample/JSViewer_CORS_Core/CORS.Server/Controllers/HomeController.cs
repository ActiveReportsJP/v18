using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace JSViewer_CORS_Core.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet("reports")]
        public ActionResult Reports()
        {
            string[] validExtensions = {".rdl", ".rdlx", ".rdlx-master"};

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