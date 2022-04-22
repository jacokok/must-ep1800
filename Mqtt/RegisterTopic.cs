using System.Text.Json.Serialization;

namespace Must.Mqtt;

public class RegisterTopic
{
    public string Name { get; set; } = "";
    public string UnitOfMeasurement { get; set; } = "";
    public string Icon { get; set; } = "";
}