using System.Text.Json;

namespace Must.Mqtt;
public static class MqttHelper
{
    public static readonly List<RegisterTopic> Topics = new() {
        new RegisterTopic { Name = "WorkStateNo", UnitOfMeasurement = "", Icon = "state-machine" },
    };

    public static readonly JsonSerializerOptions SerializeOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}