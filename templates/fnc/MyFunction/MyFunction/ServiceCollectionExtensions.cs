using MyFunction.Domain;

namespace MyFunction;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddSingleton<IMyFunctionLogic, MyFunctionLogic>();
    }
}
