using System;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Database;
using WebApp.Services;

namespace WebApp.Test
{
    public class WebAppTestEnvironment : IDisposable
    {
        public WebAppTestHost WebAppHost { get; }

        public WebAppTestEnvironment()
        {
            WebAppHost = new WebAppTestHost();
        }

        public void Start()
        {
            WebAppHost.Start();
        }

        public void Prepare()
        {
            WebAppHost.Services.GetRequiredService<IAccountCache>().Clear();
            WebAppHost.Services.GetRequiredService<IAccountDatabase>().ResetAsync().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            WebAppHost?.Dispose();
        }
    }
}