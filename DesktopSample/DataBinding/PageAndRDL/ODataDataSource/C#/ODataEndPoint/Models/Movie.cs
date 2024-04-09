using System.ComponentModel.DataAnnotations;

namespace ODataDataSource.Models
{
	public class Movie
	{
		[Key]
		public long Id { get; set; }
		public string Title { get; set; }
		public string MPAA { get; set; }
		public long YearReleased { get; set; }
	}
}
