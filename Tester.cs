using System.IO.Ports;
using Microsoft.Extensions.Options;
using Must.Modbus;

namespace Must;
public class Tester
{
    private readonly Config _config;
    private readonly ILogger<Poller> _logger;

    public Tester(IOptions<Config> config, ILogger<Poller> logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    public void Test()
    {
        try
        {
            using var port = new SerialPort(_config.PortName, 19200, Parity.None, 8, StopBits.One);
            port.Handshake = Handshake.None;
            port.DtrEnable = false;
            port.RtsEnable = false;
            port.ReadTimeout = 1500;

            _logger.LogInformation("Opening port {port}", port);
            port.Open();

            var wrapper = new SerialPortWrapper(port, d =>
            {
                var renderedBytes = string.Join(" ", d.Select(s => $"{s:X2}"));
                _logger.LogInformation("< {renderedBytes}", renderedBytes);
            },
                    d =>
                    {
                        var renderedBytes = string.Join(" ", d.Select(s => $"{s:X2}"));
                        _logger.LogInformation("> {renderedBytes}", renderedBytes);
                    });
            var reader = new ModbusReader(wrapper);

            ReadValues(reader, port, 4, 20000, 7);

            port.Close();
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error {message}", ex.Message);
            throw;
        }
    }

    private ushort[] ReadValues(ModbusReader reader, SerialPort port, byte deviceId, ushort address, ushort count)
    {
        //Sleep between reads
        Thread.Sleep(200);

        port.DiscardInBuffer();
        port.DiscardOutBuffer();

        //Set read timeout based on baud rate and count of items
        double timeout = (1.0f / (double)port.BaudRate * 1000.0 * 12.0 * count * 2.0) + (count * 2) + 5.0 + 1000.0;

        port.ReadTimeout = (int)timeout;

        _logger.LogInformation("Reading {deviceId}:{address}:{count} ... wait for {ReadTimeout}ms .... ", deviceId, address, count, port.ReadTimeout);

        ushort[] values = Array.Empty<ushort>();

        try
        {
            var start = Environment.TickCount;

            values = reader.Read(deviceId, address, count);

            var end = Environment.TickCount;

            _logger.LogInformation("got {Length} values in {Diff} ms", values.Length, end - start);
        }
        catch (TimeoutException)
        {
            _logger.LogInformation("Timeout Exception: {BytesToRead} bytes are available to read.", port.BytesToRead);
        }
        catch (InvalidDataException ex)
        {
            _logger.LogInformation("Invalid Data Exception:  {message}", ex.Message);
        }

        _logger.LogInformation("");

        return values;
    }
}