[assembly: FunctionsStartup(typeof(MyFunction.Startup))]
namespace MyFunction;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration as IConfigurationRoot;
        builder.Services.RegisterServices(configuration);
    }
}
