using Cryptocurrency.Application.Handlers;
using Cryptocurrency.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cryptocurrency.Application.Services
{
    public class AppLuncher : IAppLuncher
    {
        private readonly ILogger<AppLuncher> logger;
        private readonly CryptocurrencyHandler cryptocurrencyHandler;

        public AppLuncher(ILogger<AppLuncher> logger, CryptocurrencyHandler cryptocurrencyHandler)
        {
            this.cryptocurrencyHandler = cryptocurrencyHandler ?? throw new ArgumentNullException(nameof(cryptocurrencyHandler));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task LunchApp()
        {
            logger.LogDebug("Application lunched");

            bool showMenu = true;
            while (showMenu) 
            {
                showMenu = await ShowMenu();
            }
        }

        private async Task<bool> ShowMenu()
        {
            Console.Write("Enter a Cryptocurrency Symbol (e.g. BTC) or (Q) for Exit: ");

            var input = Console.ReadLine();

            if (input.ToLower() == "q")
                return false;
            else
            {
                var symbols = await cryptocurrencyHandler.GetCryptoSymbols();
                Console.WriteLine("OK");
                return true;
            }

        }
    }
}
