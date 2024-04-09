using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Web.OData;

using ODataDataSource.Models;

namespace ODataDataSource.Controllers
{
	/// <summary>
	/// Controller is based on article https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
	/// </summary>
	public class MoviesController : ODataController
	{
		public  IList<Movie> Get()
		{
			var connStr = Utility.UpdateConnectionString("$appPath$..\\..\\..\\..\\..\\Data\\movie.json");		
			var jsonString = File.ReadAllText(connStr);
			var movies = JsonSerializer.Deserialize<List<Movie>>(jsonString);

			return movies;
		}

	}
}
