using System.IO.Ports;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Must.Modbus;
using Must.Models;

namespace Must;
public class Poller
{
    private readonly Config _config;
    private readonly ILogger<Poller> _logger;

    public Poller(IOptions<Config> config, ILogger<Poller> logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    public string GetJSON()
    {
        try
        {
            return "test";
            using var port = new SerialPort(_config.PortName, 19200, Parity.None, 8, StopBits.One);
            port.Handshake = Handshake.None;
            port.DtrEnable = false;
            port.RtsEnable = false;
            port.ReadTimeout = 1500;

            _logger.LogInformation($"Opening port {port}");
            port.Open();

            var wrapper = new SerialPortWrapper(
                port, d => { },
                d => { }
            );
            var reader = new ModbusReader(wrapper);

            ushort[] values;
            EP1800 modelPh = new();

            values = ReadValues(reader, port, 4, 10001, 8);
            SensorToModelMapper.Map(10001, values, modelPh);

            values = ReadValues(reader, port, 4, 10103, 10);
            SensorToModelMapper.Map(10103, values, modelPh);

            values = ReadValues(reader, port, 4, 15201, 21);
            SensorToModelMapper.Map(15201, values, modelPh);

            values = ReadValues(reader, port, 4, 20000, 17);
            SensorToModelMapper.Map(20000, values, modelPh);

            values = ReadValues(reader, port, 4, 20101, 43);
            SensorToModelMapper.Map(20101, values, modelPh);

            values = ReadValues(reader, port, 4, 25201, 79);
            SensorToModelMapper.Map(25201, values, modelPh);

            var json = JsonSerializer.Serialize<EP1800>(modelPh, new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true,
            });

            _logger.LogInformation("======");
            _logger.LogInformation(json);

            port.Close();
            return json;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Error {ex.Message}");
            throw;
        }
    }

    private static ushort[] ReadValues(ModbusReader reader, SerialPort port, byte deviceId, ushort address, ushort count)
    {
        port.DiscardInBuffer();
        port.DiscardOutBuffer();

        double timeout = (1.0f / (double)port.BaudRate * 1000.0 * 12.0 * count * 2.0) + (count * 2) + 5.0 + 1000.0;

        port.ReadTimeout = (int)timeout;
        return reader.Read(deviceId, address, count);
    }
}