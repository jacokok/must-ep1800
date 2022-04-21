using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace Must.Modbus
{
    public class SerialPortWrapper : ISerialPort
    {
        private readonly SerialPort _serialPort;
        private readonly Action<byte[]> _dataInFunc;
        private readonly Action<byte[]> _dataOutFunc;

        public SerialPortWrapper(SerialPort serialPort, Action<byte[]> dataIn, Action<byte[]> dataOut)
        {
            _serialPort = serialPort;
            _dataInFunc = dataIn;
            _dataOutFunc = dataOut;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            var start = Environment.TickCount;
            var current = Environment.TickCount;
            var total = 0;
            var chunk = new byte[32];
            while (total < count && (current - start < _serialPort.ReadTimeout || _serialPort.ReadTimeout == -1))
            {
                var read = _serialPort.Read(chunk, 0, chunk.Length);
                Buffer.BlockCopy(chunk, 0, buffer, offset + total, read);
                total += read;
                Thread.Sleep(10);
                current = Environment.TickCount;
            }

            _dataInFunc(buffer);
            return total;
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            _dataOutFunc(buffer);
            _serialPort.Write(buffer, offset, count);
        }
    }
}
