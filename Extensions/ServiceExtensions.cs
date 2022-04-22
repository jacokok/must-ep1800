using Must.Mqtt;

namespace Must.Extensions;
public static class ServiceExtensions
{
    public static void ConfigureMQTTClient(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddSingleton<MqttClient>();
    }
}