using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using scraperel.api.Common;
using scraperel.model;
using scraperel.scraper;
using Serilog;

namespace scraperel.api
{
	[ApiController]
	[Route("/")]
	public class DefaultController : ControllerBase
	{
		readonly ScraperDbContext dbContext;

		public DefaultController(ScraperDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public OkObjectResult GetStartScraping()
		{
			return Ok("Here you can start scraping");
		}

		[HttpPost("scrape")]
		public async Task<IEnumerable<MenuItem>> PostStartScraping([FromBody] MenuStarter menuStarter)
		{
			Log.Logger.Information($"scraping menu from {menuStarter.MenuUrl}...");

			Scraper scraper = new Scraper(new ParserConfig());

			var items = await scraper.Scrape(menuStarter.MenuUrl);

			await dbContext.MenuItems.AddRangeAsync(items);
			await dbContext.SaveChangesAsync();

			return items;

		}
	}
}