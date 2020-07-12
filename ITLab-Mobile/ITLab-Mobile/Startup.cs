using ITLab_Mobile.Services;
using ITLab_Mobile.Services.Helpers;
using ITLab_Mobile.ViewModels.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Xamarin.Essentials;

namespace ITLab_Mobile
{
    public static class Startup
    {
        public static App Init()
        {
            var systemDir = FileSystem.CacheDirectory;
            var namespaceName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            Utils.ExtractSaveResource($"{namespaceName}.Data.appsettings.json", systemDir);
            var fullConfig = Path.Combine(systemDir, $"{namespaceName}.Data.appsettings.json");

            var host = new HostBuilder()
                            .ConfigureHostConfiguration(c =>
                            {
                                c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                                c.AddJsonFile(fullConfig);
                            })
                            .ConfigureServices((c, x) =>
                            {
                                ConfigureServices(c, x);
                            })
                            .ConfigureLogging(l => l.AddConsole(o =>
                            {
                                o.DisableColors = true;
                            }))
                            .Build();

            App.ServiceProvider = host.Services;

            return App.ServiceProvider.GetService<App>();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddTransient<MessageDelegatingHandler>(cfg =>
            {
                return new MessageDelegatingHandler(
                    Settings.OidcClient,
                    Settings.AccessToken,
                    Settings.RefreshToken
                );
            });
            services.AddTransient<EventViewModel>();

            services.AddHttpClient("test_name", cfg =>
                {
                    cfg.BaseAddress = new Uri(Settings.ApiOptions.BaseUrl);
                })
                .AddHttpMessageHandler<MessageDelegatingHandler>();

            services.AddSingleton<App>();
        }
    }
}
