using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebDesigner_MVC.Controllers
{
	public class DesignController : Controller
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
