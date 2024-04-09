using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace JSViewer_React_Hooks.Controllers
{
    [Route("[controller]")]
    public class ReportsListController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string[] validExtensions = { ".rdl", ".rdlx", ".rdlx-master" };
            return GetFileStoreReports(validExtensions);
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
