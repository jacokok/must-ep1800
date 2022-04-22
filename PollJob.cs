using Microsoft.Extensions.Options;
using Must.Mqtt;
using Quartz;

namespace Must;

public class PollJob : IJob
{
    private readonly ILogger<PollJob> _logger;
    private readonly Config _config;
    private readonly Poller _poller;
    private readonly Tester _tester;
    private readonly MqttClient _mqttClient;
    public PollJob(ILogger<PollJob> logger, IOptions<Config> config, Poller poller, Tester tester, MqttClient mqttClient)
    {
        _logger = logger;
        _config = config.Value;
        _poller = poller;
        _tester = tester;
        _mqttClient = mqttClient;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (_config.IsTest)
        {
            _logger.LogInformation("Starting test on port {PortName}", _config.PortName);
            _tester.Test();
            _logger.LogInformation("===== Finished Test =====");
        }
        else
        {
            string json = _poller.GetJSON();
            _logger.LogInformation("{json}", json);

            foreach (PublishPayload payload in MqttHelper.JsonToPublishList(json))
            {
                await _mqttClient.Publish(payload.Topic, payload.Value);
            }
        }
    }
}