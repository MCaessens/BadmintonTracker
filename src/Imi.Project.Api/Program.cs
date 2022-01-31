using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Imi.Project.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder

                        //  EXTERNAL TESTING
                        .UseUrls("http://0.0.0.0:7000", "https://0.0.0.0:7001")
                        .UseStartup<Startup>();
                });
    }
}