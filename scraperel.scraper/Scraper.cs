using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using scraperel.model;
using Serilog;

namespace scraperel.scraper
{
	public class Scraper
	{
		const string SPAN = "</span>";
		readonly ParserConfig parserConfig;
		readonly IBrowsingContext context;

		public Scraper(ParserConfig parserConfig)
		{
			this.parserConfig = parserConfig;
			var config = Configuration.Default.WithDefaultLoader();
			context = BrowsingContext.New(config);
		}

		public async Task<List<MenuItem>> Scrape(string url)
		{
			var document = await context.OpenAsync(url);
			var list = new List<MenuItem>();

			var menuSections = document.QuerySelectorAll(parserConfig.MenuSectionRoot);
			var menuDescription = document.QuerySelector(parserConfig.MenuDescriptionPath).TextContent
			                              .Replace("<br>", "", StringComparison.Ordinal);
			var menuTitle = document.QuerySelector(parserConfig.MenuTitlePath).TextContent;

			foreach (var menuSection in menuSections)
			{
				var menuSectionTitle = menuSection.QuerySelector("span").TextContent;

				var menuSectionId = menuSection.Attributes["href"].Value.Substring(1);

				var dishesPerSection = document.QuerySelectorAll($"div[id='{menuSectionId}']");

				foreach (var dishPerSection in dishesPerSection)
				{
					var dishes = dishPerSection.QuerySelectorAll($"{parserConfig.DishRootPath}");

					foreach (var dish in dishes)
					{
						var dishDetailsUrl = ((IHtmlAnchorElement) dish.QuerySelector("a")).Href;

						Log.Logger.Debug($"dishDetailsUrl {dishDetailsUrl}");

						var dishDetailsPage = await context.OpenAsync(dishDetailsUrl);

						var dishDescription = dishDetailsPage.QuerySelector(parserConfig.DishDescriptionPath).TextContent;

						var dishHtml = dish.QuerySelector(parserConfig.DishNamePath).InnerHtml;
						var dishName = dishHtml.Substring(dishHtml.IndexOf(SPAN, StringComparison.Ordinal)
						                                + SPAN.Length + 1 );

						var menuItem = new MenuItem();
						menuItem.MenuTitle = menuTitle;
						menuItem.MenuSectionTitle = menuSectionTitle;
						menuItem.DishName = dishName;
						menuItem.MenuDescription = menuDescription;
						menuItem.DishDescription = dishDescription;

						list.Add(menuItem);
					}
				}
			}

			return list;

		}
	}
}