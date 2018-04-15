using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Scrummy.Runtime.Common.Initialization;

namespace Scrummy.Application.Web.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RuntimeInitializer.Initialize();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
