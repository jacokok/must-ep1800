namespace Must;

public class Config
{
    public string PortName { get; set; } = "/dev/ttyUSB0";
    public string Cron { get; set; } = "0 0/5 * * * ?";
    public bool IsTest { get; set; }
    public string MqttServer { get; set; } = "";
    public string MqttPort { get; set; } = "1883";
    public string MqttTopic { get; set; } = "homeassistant";
    public string MqttDeviceName { get; set; } = "must-inverter";
    public string MqttUserName { get; set; } = "";
    public string MqttPassword { get; set; } = "";
}