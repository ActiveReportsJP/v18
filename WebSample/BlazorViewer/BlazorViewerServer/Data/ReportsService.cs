using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorViewerServer.Data
{
    public class ReportsService
    {
        public IEnumerable<string> GetReports()
        {
            string[] validExtensions = { ".rdl", ".rdlx", ".rdlx-master", ".rpx" };
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
