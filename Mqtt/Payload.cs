using System.Text.Json.Serialization;

namespace Must.Mqtt;

public class Payload
{
    public string Name { get; set; } = "";
    [JsonPropertyName("unit_of_measurement")]
    public string UnitOfMeasurement { get; set; } = "";
    [JsonPropertyName("state_topic")]
    public string Topic { get; set; } = "";
    public string Icon { get; set; } = "";
    [JsonPropertyName("device_class")]
    public string? DeviceClass { get; set; }
    [JsonPropertyName("state_class")]
    public string? StateClass { get; set; }
}