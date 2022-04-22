using MQTTnet.Client.Options;

namespace Must.Extensions;
public static class ServiceExtensions
{
    public static void ConfigureMQTTClient(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddSingleton<IMqttClientOptions>();
    }
}