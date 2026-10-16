using System;
using System.IO;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace Git.Training
{
	public class Program
	{
		#region -= Properties =-
		public static IConfiguration Configuration { get; set; }
		#endregion

		public static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();

			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseUrls(Configuration.GetSection("HostingURL").Value)
				.UseIISIntegration()
				.UseIIS()
				.ConfigureLogging(logging => {
					logging.AddApplicationInsights(Configuration.GetSection("ApplicationInsights:InstrumentationKey").Value);
					logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);
				});        
	}
}