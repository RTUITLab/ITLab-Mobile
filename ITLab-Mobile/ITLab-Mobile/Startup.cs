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
using Xamarin.Forms;

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
            services.AddSingleton<MessageDelegatingHandler>(cfg =>
            {
                return new MessageDelegatingHandler(
                    Settings.OidcClient,
                    string.IsNullOrEmpty(Settings.AccessToken) ? "default" : Settings.AccessToken,
                    string.IsNullOrEmpty(Settings.RefreshToken) ? "default" : Settings.RefreshToken
                );
            });

            services.AddHttpClient(Settings.HttpClientName, cfg =>
                {
                    cfg.BaseAddress = new Uri(Settings.ApiOptions.BaseUrl);
                    cfg.DefaultRequestHeaders.Add("User-Agent", UserAgent());
                })
                .AddHttpMessageHandler<MessageDelegatingHandler>();

            services.AddSingleton<App>();
        }

        private static string UserAgent()
        {
            //TODO: Get correct app version
            var version = $"1.0.0";
            return $"Xamarin.{Device.RuntimePlatform}/{version}";
        }
    }
}
