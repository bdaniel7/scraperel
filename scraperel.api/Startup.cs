using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using scraperel.model;
using Serilog;

namespace scraperel.api
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddOpenApiDocument();
			services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
			services.AddHttpClient();

			var scraperCS = Configuration.GetConnectionString("scraperel");
			var scraperAssemblyName = typeof(ScraperDbContext).Assembly.GetName().Name;

			services.AddDbContext<ScraperDbContext>(options =>
			                                        {
				                                        options.UseSqlServer(scraperCS,
						                                        sql => sql.MigrationsAssembly(scraperAssemblyName)
						                                        ).EnableSensitiveDataLogging();
			                                        }).AddDbContextPool<ScraperDbContext>(options =>
																								{
																									options.UseSqlServer(scraperCS);
																								});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//app.UseHttpsRedirection();

			app.UseRouting();
			app.UseOpenApi();
			app.UseSwaggerUi3();
			app.UseReDoc();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}