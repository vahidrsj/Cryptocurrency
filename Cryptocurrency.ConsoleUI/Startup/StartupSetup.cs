using Cryptocurrency.Application.APISettings;
using Cryptocurrency.Application.Common;
using Cryptocurrency.Application.Handlers;
using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Application.Services;
using Cryptocurrency.Infrastructure.Services;
using Cryptocurrency.Infrastructure.Services.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;

namespace Cryptocurrency.ConsoleUI.Startup
{
    public class StartupSetup
    {
        public static IConfiguration Configuration { get; }

        static StartupSetup()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = config.Build();
        }

        public ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var coinMarketSetting = new CoinMarketAPISetting();
            Configuration.GetSection("CoinMarketSetting").Bind(coinMarketSetting);

            var exchangeRateSetting = new ExchangeRateAPISetting();
            Configuration.GetSection("ExchangeRateSetting").Bind(exchangeRateSetting);

            services.Configure<CurrencySetting>(c => Configuration.GetSection("CurrencySetting").Bind(c));

            services.AddRefitClient<ICoinMarketAPI>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(coinMarketSetting.BaseURL));

            services.AddRefitClient<IExchangeRateAPI>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(exchangeRateSetting.BaseURL));

            services.AddMemoryCache();

            services.AddTransient<ICryptoListService, CryptoListService>();
            services.AddTransient<ICryptoPriceService, CryptoPriceService>();

            services.AddTransient<ICryptocurrencyHandler, CryptocurrencyHandler>();
            services.AddTransient<IAppLuncher, AppLuncher>();

            var appConfigSettings = new AppConfig();
            Configuration.GetSection("AppConfig").Bind(appConfigSettings);

            if (appConfigSettings.Environment.ToLower() == "developement")
                services.AddLogging(configure => configure.AddConsole())
                     .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                    .AddScoped<CryptocurrencyHandler>();
            else
                services.AddLogging(configure => configure.AddConsole())
                     .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Error)
                    .AddScoped<CryptocurrencyHandler>();

            return services.BuildServiceProvider();
        }
    }
}
