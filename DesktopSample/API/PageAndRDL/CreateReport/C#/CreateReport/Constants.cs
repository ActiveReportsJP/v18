
namespace ActiveReports.Samples.CreateReport
{
	internal sealed class Constants
	{
		public const string cmdText = 
			"SELECT Movie.MovieID, Movie.Title, Movie.YearReleased, Movie.MPAA FROM Movie ORDER BY Movie.YearReleased";
	}
}
