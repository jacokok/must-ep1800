using System.Text.Json.Serialization;

namespace Must.Mqtt;

public class PublishPayload
{
    public string Topic { get; set; } = "";
    public string Value { get; set; } = "";
}