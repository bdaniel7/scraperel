namespace scraperel.scraper
{
	public class ParserConfig
	{
		public string MenuRoot { get; set; } = "ul.submenu a.active";

		public string MenuTitlePath { get; set; } = "ul.submenu a.active";
		public string MenuDescriptionPath { get; set; } = "header.menu-header p";

		public string MenuSectionRoot { get; set; } = "h4.menu-title a";
		public string MenuSectionTitlePath { get; set; } = "h4.menu-title span";

		public string DishRootPath { get; set; } = "div.menu-item";

		public string DishNamePath { get; set; } = "div.menu-item h3";
		public string DishDescriptionPath { get; set; }

	}
}