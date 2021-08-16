using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KestrelEndpointsDemo
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
                    webBuilder.UseStartup<Startup>().UseKestrel(opts =>
                    {
                        opts.ListenLocalhost(10000, opts =>opts.Protocols= HttpProtocols.Http1);
                        opts.ListenLocalhost(10001, opts => opts.UseHttps());
                    });
                    // UseUrls 相關設定會被 UseKestrel 覆蓋
                    webBuilder.UseUrls("http://localhost:8000", "https://localhost:8001");
                });
    }
}