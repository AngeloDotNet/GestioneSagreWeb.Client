using Blazored.LocalStorage;
using GestioneSagre.Web.Services.Generico;

namespace GestioneSagre.Web.App;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = false;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

        var endpointAddress = $"http://{builder.Configuration["EndpointAPI:Default"]}";
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(endpointAddress) });

        builder.Services.Scan(scan => scan.FromAssemblyOf<IGenericoService>()
            .AddClasses(services => services.Where(type => type.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        await builder.Build().RunAsync();
    }
}