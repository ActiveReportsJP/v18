using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebDesigner_Custom.Data;
using WebDesigner_Custom.Services;

namespace WebDesigner_Custom.Controllers
{
	[Route("/")]
	public class DesignController : Controller
	{
		private readonly ReportService _reportService;
		public DesignController(ReportService reportService)
		{
			_reportService = reportService;
		}
		
		[HttpGet("reports")]
		public List<Report> Reports()
		{
			return _reportService.GetReports().ToList();
		}
	}
}
