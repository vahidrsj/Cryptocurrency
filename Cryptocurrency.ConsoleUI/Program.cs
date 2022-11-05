using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.ConsoleUI.Startup;
using Microsoft.Extensions.DependencyInjection;

var startup = new StartupSetup();
var sp = startup.ConfigureServices();

var applicationService = sp.GetService<IAppLuncher>();
if (applicationService != null)
    await applicationService.LunchApp();
