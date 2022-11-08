using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.ConsoleUI.Startup;
using Microsoft.Extensions.DependencyInjection;

//Conigure the startup services
var startup = new StartupSetup();
var sp = startup.ConfigureServices();

//Launching the app async
var applicationService = sp.GetService<IAppLauncher>();
if (applicationService != null)
    await applicationService.LauncheApp();
