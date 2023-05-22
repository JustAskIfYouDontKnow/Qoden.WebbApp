﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApp
{
    static internal class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                       .UseStartup<Startup>();
    }
}