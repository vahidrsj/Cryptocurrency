using Cryptocurrency.Application.APISettings;
using Cryptocurrency.Application.Dto.ServiceResultDto;
using Cryptocurrency.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cryptocurrency.Application.Services
{
    public class AppLauncher : IAppLauncher
    {
        private readonly ICryptocurrencyHandler cryptocurrencyHandler;
        private readonly CurrencySetting option;

        public AppLauncher(ICryptocurrencyHandler cryptocurrencyHandler, IOptions<CurrencySetting> option)
        {
            this.cryptocurrencyHandler = cryptocurrencyHandler ?? throw new ArgumentNullException(nameof(cryptocurrencyHandler));
            this.option = option.Value ?? throw new ArgumentNullException(nameof(option));
        }

        /// <summary>
        /// Application starts with this methode.
        /// </summary>
        /// <returns></returns>
        public async Task LauncheApp()
        {
            Console.Clear();
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await ShowMenu();
            }
        }

        private async Task<bool> ShowMenu()
        {
            Console.WriteLine("Cryptocurrency");
            Console.WriteLine("Choose one of the items below by entering the number / letter beside: \r\n");
            Console.WriteLine("[1]: List of Cryptocurrencies");
            Console.WriteLine("[2]: Specific crypto prices");
            Console.WriteLine("[q]: exit");

            var input = Console.ReadLine();

            if (input?.ToLower() == "q")
                return false;
            else if (input == "1")
            {
                var symbols = await cryptocurrencyHandler.GetCryptoSymbols();
                if (!symbols.IsSuccessfull)
                    Console.WriteLine(symbols.ErrorInfo.ToString());
                else
                    displayCryptoList(symbols.Result);

                return true;
            }
            else if (input == "2")
            {
                Console.Clear();
                Console.WriteLine("Enter a crypto symbol (e.g. BTC):");
                var symbol = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(symbol))
                {
                    Console.WriteLine("\r\n Invalid symbol. \r\n");
                }
                else
                {
                    var crypto = await cryptocurrencyHandler.GetCryptoPrices(symbol.ToUpper(), option.Currencies);
                    if (!crypto.IsSuccessfull)
                        Console.WriteLine(crypto.ErrorInfo.ToString() + "\r\n");
                    else
                    {
                        Console.WriteLine($"Cryptocurrency name: {crypto.Result.Name}\r\nSymbol: {crypto.Result.Symbol}\r\n" +
                            $"Prices:\r\n{string.Join("\r\n", crypto.Result.Prices.Select(d => d.Key + " = " + d.Value).ToArray())} \r\n");
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("\r\n Invalid character. \r\n");
                return true;
            }
        }

        private void displayCryptoList(CryptoListDto cryptoList)
        {
            Console.WriteLine("Top 30 Cryptocurrencies by rank: \r\n");
            foreach (var item in cryptoList.Data)
            {
                Console.WriteLine($"{item.name} ({item.symbol})");
            }
            Console.WriteLine();
        }
    }
}
