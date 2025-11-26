
namespace CodingProblemsRepl.Api.Utilities;
public static class ServiceRegistration
{
    public static IServiceCollection RegisterUtilities(this IServiceCollection services)
    {
        services.AddSingleton<FileEncryptor>();

        return services;
    }
}