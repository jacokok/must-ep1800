namespace Must;

public class Config
{
    public string PortName { get; set; } = "/dev/ttyUSB0";
    public string Cron { get; set; } = "0 0/5 * * * ?";
}