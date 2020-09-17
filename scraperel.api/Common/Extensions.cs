using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace scraperel.api.Common
{
	public static class Extensions
	{
		public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
		{
			var model = new TModel();

			var configurationSection = configuration.GetSection(section);

			configurationSection.Bind(model);

			return model;
		}

		public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder)
		{
			return webHostBuilder.UseSerilog((context, loggerConfiguration) =>
                           {
                             var seqOptions = context.Configuration.GetOptions<SeqOptions>("seq");
                             var serilogOptions = context.Configuration.GetOptions<SerilogOptions>("serilog");

                             if (!Enum.TryParse<LogEventLevel>(serilogOptions.Level, true, out var level))
                             {
                               level = LogEventLevel.Information;
                             }

                             loggerConfiguration.Enrich.FromLogContext()
                                                .MinimumLevel.Is(level)
                                                .Enrich.WithProperty("Environment",
                                                     context.HostingEnvironment.EnvironmentName)
                                                .WriteTo.RollingFile("log/screperel-api.txt");
                             ;
                             configure(loggerConfiguration, seqOptions, serilogOptions);
                           });
		}

		static void configure(LoggerConfiguration loggerConfiguration, SeqOptions seqOptions, SerilogOptions serilogOptions)
		{

			if (seqOptions.Enabled)
			{
				loggerConfiguration.WriteTo.Seq(seqOptions.Url, apiKey: seqOptions.ApiKey);
			}

			if (serilogOptions.ConsoleEnabled)
			{
				loggerConfiguration.WriteTo.Console(
						outputTemplate:
						"[{Timestamp:HH:mm:ss} {Level}] {Environment} {MachineName} {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
						theme: AnsiConsoleTheme.Literate);
			}
		}
	}
}