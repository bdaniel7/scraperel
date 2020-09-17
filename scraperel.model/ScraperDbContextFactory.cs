using System;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace scraperel.model
{
	public class ScraperDbContextFactory : IDesignTimeDbContextFactory<ScraperDbContext>
	{
		public ScraperDbContext CreateDbContext(string[] args)
		{
			string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			IConfigurationRoot configuration = new ConfigurationBuilder()
			                                  .SetBasePath(Directory.GetCurrentDirectory())
			                                  .AddJsonFile($"appsettings.{environment}.json", true)
			                                  .AddJsonFile("appsettings.json")
			                                  .AddEnvironmentVariables()
			                                  .Build();

			var builder = new DbContextOptionsBuilder<ScraperDbContext>();

			var connectionString = configuration.GetConnectionString("scraperel");

			builder.UseSqlServer(connectionString,
					sql =>
					{
						sql.MigrationsAssembly(typeof(ScraperDbContext).Assembly.GetName().Name);
					}).EnableSensitiveDataLogging();

			SqlConnectionStringBuilder cdb = new SqlConnectionStringBuilder(connectionString);

			Log.Information($"{cdb.DataSource}, {cdb.InitialCatalog}, {cdb.UserID}");

			return new ScraperDbContext(builder.Options);
		}
	}
}