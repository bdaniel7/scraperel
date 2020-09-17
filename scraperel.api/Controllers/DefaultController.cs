using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using scraperel.api.Common;
using scraperel.model;
using Serilog;

namespace scraperel.api
{
	[ApiController]
	[Route("[controller]")]
	[Route("/")]
	public class DefaultController : ControllerBase
	{
		[HttpGet]
		public OkObjectResult GetStartScraping()
		{
			return Ok("Here you can start scraping");
		}

		[HttpPost("scrape")]
		public IEnumerable<MenuItem> PostStartScraping([FromBody] MenuStarter menuStarter)
		{
			Log.Logger.Information($"scraping menu from {menuStarter.MenuUrl}...");

			return new List<MenuItem>()
			       {
					       new MenuItem
					       {
							       MenuTitle = "Breakfast",
							       MenuDescription =
									       "Our nutritious breakfasts are served in seconds and last until lunch...",
							       MenuSectionTitle = "Super Eggs",
							       DishName = "Super Eggs",
							       DishDescription =
									       "Free-range egg omelette, rolled and filled with avocado, roquito peppers, edamame, spinach and free-range egg mayonnaise.",
					       },
					       new MenuItem
					       {
							       MenuTitle = "Breakfast",
							       MenuDescription = "Our nutritious breakfasts are served in seconds and last until lunch...",
							       MenuSectionTitle = "Super Eggs",
							       DishName = "Super Eggs with Vine tomatoes",
							       DishDescription =
									       "Three free-range eggs scrambled with wilted spinach and petit pois, served with Vine Tomatoes.",
					       },
			       };
		}
	}
}