using Must.Mqtt;

namespace Must.Extensions;
public class StartupHostedService : IHostedService
{
    private readonly ILogger<StartupHostedService> _logger;
    private readonly MqttClient _mqttClient;
    public StartupHostedService(ILogger<StartupHostedService> logger, MqttClient mqttClient)
    {
        _logger = logger;
        _mqttClient = mqttClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _mqttClient.Init();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
