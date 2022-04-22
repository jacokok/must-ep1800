using System.Text.Json;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;

namespace Must.Mqtt;

public class MqttClient
{
    private readonly Config _config;
    private readonly ILogger<Poller> _logger;

    public MqttClient(IOptions<Config> config, ILogger<Poller> logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    public MqttClientOptions GetMqttClientOptions()
    {
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(_config.MqttServer, int.Parse(_config.MqttPort));

        if (!string.IsNullOrEmpty(_config.MqttUserName) && !string.IsNullOrEmpty(_config.MqttPassword))
        {
            mqttClientOptions.WithCredentials(_config.MqttUserName, _config.MqttPassword);
        }
        return mqttClientOptions.Build();
    }

    public async Task Init()
    {
        try
        {
            var mqttFactory = new MqttFactory();
            using var mqttClient = mqttFactory.CreateMqttClient();
            var response = await mqttClient.ConnectAsync(GetMqttClientOptions(), CancellationToken.None);

            if (response.ResultCode != MqttClientConnectResultCode.Success)
            {
                _logger.LogError("Failed to connect to MQTT");
                return;
            }

            foreach (RegisterTopic topic in MqttHelper.Topics)
            {
                Payload p = new()
                {
                    Name = $"{_config.MqttDeviceName}_{topic.Name}",
                    Topic = $"{_config.MqttTopic}/sensor/{_config.MqttDeviceName}/{topic.Name}",
                    Icon = $"mdi:{topic.Icon}",
                    UnitOfMeasurement = topic.UnitOfMeasurement
                };

                string json = JsonSerializer.Serialize(p, MqttHelper.SerializeOptions);

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic($"{_config.MqttTopic}/sensor/{_config.MqttDeviceName}/{topic.Name}/config")
                    .WithPayload(json)
                    .WithRetainFlag()
                    .Build();

                await mqttClient.PublishAsync(message, CancellationToken.None);
            }

            var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();
            await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MQTT Problem");
        }
    }

    public async Task Publish(string Topic, string Value)
    {
        try
        {
            var mqttFactory = new MqttFactory();
            using var mqttClient = mqttFactory.CreateMqttClient();
            var response = await mqttClient.ConnectAsync(GetMqttClientOptions(), CancellationToken.None);

            if (response.ResultCode != MqttClientConnectResultCode.Success)
            {
                _logger.LogError("Failed to connect to MQTT");
                return;
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic($"{_config.MqttTopic}/sensor/{_config.MqttDeviceName}/{Topic}")
                .WithPayload(Value)
                .WithRetainFlag()
                .Build();

            await mqttClient.PublishAsync(message, CancellationToken.None);

            var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();
            await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MQTT Problem");
        }
    }

}