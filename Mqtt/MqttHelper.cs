using System.Text.Json;
using System.Text.Json.Serialization;
using Must.Models;

namespace Must.Mqtt;
public static class MqttHelper
{
    public static readonly List<RegisterTopic> Topics = new() {
        new RegisterTopic { Name = "WorkStateNo", UnitOfMeasurement = "", Icon = "state-machine" },
        new RegisterTopic { Name = "AcVoltageGrade", UnitOfMeasurement = "V", Icon = "current-ac", DeviceClass="voltage", StateClass="measurement" },
        new RegisterTopic { Name = "RatedPower", UnitOfMeasurement = "VA", Icon = "lightbulb-outline", DeviceClass="apparent_power", StateClass="measurement" },
        new RegisterTopic { Name = "BatteryVoltage", UnitOfMeasurement = "V", Icon = "current-dc", DeviceClass="voltage", StateClass="measurement" },
        new RegisterTopic { Name = "InverterVoltage", UnitOfMeasurement = "V", Icon = "current-ac", DeviceClass="voltage", StateClass="measurement" },
        new RegisterTopic { Name = "GridVoltage", UnitOfMeasurement = "V", Icon = "current-ac", DeviceClass="voltage", StateClass="measurement" },
        new RegisterTopic { Name = "BusVoltage", UnitOfMeasurement = "V", Icon = "cog-transfer-outline", DeviceClass="voltage", StateClass="measurement" },
        new RegisterTopic { Name = "ControlCurrent", UnitOfMeasurement = "A", Icon = "current-ac", DeviceClass="current", StateClass="measurement" },
        new RegisterTopic { Name = "InverterCurrent", UnitOfMeasurement = "A", Icon = "current-ac", DeviceClass="current", StateClass="measurement" },
        new RegisterTopic { Name = "GridCurrent", UnitOfMeasurement = "A", Icon = "current-ac", DeviceClass="current", StateClass="measurement" },
        new RegisterTopic { Name = "LoadCurrent", UnitOfMeasurement = "A", Icon = "current-ac", DeviceClass="current", StateClass="measurement" },
        new RegisterTopic { Name = "PInverter", UnitOfMeasurement = "W", Icon = "cog-transfer-outline", DeviceClass="power", StateClass="measurement" },
        new RegisterTopic { Name = "PGrid", UnitOfMeasurement = "W", Icon = "transmission-tower", DeviceClass="power", StateClass="measurement" },
        new RegisterTopic { Name = "PLoad", UnitOfMeasurement = "W", Icon = "lightbulb-on-outline", DeviceClass="power", StateClass="measurement" },
        new RegisterTopic { Name = "LoadPercent", UnitOfMeasurement = "%", Icon = "progress-download", DeviceClass="power_factor", StateClass="measurement" },
        new RegisterTopic { Name = "SInverter", UnitOfMeasurement = "VA", Icon = "cog-transfer-outline", DeviceClass="apparent_power", StateClass="measurement" },
        new RegisterTopic { Name = "SGrid", UnitOfMeasurement = "VA", Icon = "transmission-tower", DeviceClass="apparent_power", StateClass="measurement" },
        new RegisterTopic { Name = "SLoad", UnitOfMeasurement = "VA", Icon = "lightbulb-on-outline", DeviceClass="apparent_power", StateClass="measurement" },
        new RegisterTopic { Name = "QInverter", UnitOfMeasurement = "VA", Icon = "cog-transfer-outline", DeviceClass="apparent_power", StateClass="measurement" },
        new RegisterTopic { Name = "QGrid", UnitOfMeasurement = "VA", Icon = "transmission-tower", DeviceClass="apparent_power", StateClass="measurement" },
        new RegisterTopic { Name = "QLoad", UnitOfMeasurement = "VA", Icon = "lightbulb-on-outline", DeviceClass="apparent_power", StateClass="measurement" },
        new RegisterTopic { Name = "InverterFrequency", UnitOfMeasurement = "Hz", Icon = "sine-wave", DeviceClass="frequency", StateClass="measurement" },
        new RegisterTopic { Name = "GridFrequency", UnitOfMeasurement = "Hz", Icon = "sine-wave", DeviceClass="frequency", StateClass="measurement" },
        new RegisterTopic { Name = "InverterMaxNumber", UnitOfMeasurement = "", Icon = "format-list-numbered" },
        new RegisterTopic { Name = "CombineType", UnitOfMeasurement = "", Icon = "format-list-bulleted-type" },
        new RegisterTopic { Name = "InverterNumber", UnitOfMeasurement = "", Icon = "format-list-numbered" },
        new RegisterTopic { Name = "AcRadiatorTemp", UnitOfMeasurement = "°C", Icon = "thermometer", DeviceClass="temperature", StateClass="measurement" },
        new RegisterTopic { Name = "TransformerTemp", UnitOfMeasurement = "°C", Icon = "thermometer", DeviceClass="temperature", StateClass="measurement" },
        new RegisterTopic { Name = "DcRadiatorTemp", UnitOfMeasurement = "°C", Icon = "thermometer", DeviceClass="temperature", StateClass="measurement" },
        new RegisterTopic { Name = "InverterRelayStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "GridRelayStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "LoadRelayStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "NLineRelayStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "DcRelayStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "EarthRelayStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "Error1", UnitOfMeasurement = "", Icon = "alert-circle-outline" },
        new RegisterTopic { Name = "Error2", UnitOfMeasurement = "", Icon = "alert-circle-outline" },
        new RegisterTopic { Name = "Error3", UnitOfMeasurement = "", Icon = "alert-circle-outline" },
        new RegisterTopic { Name = "Warning1", UnitOfMeasurement = "", Icon = "alert-outline" },
        new RegisterTopic { Name = "Warning2", UnitOfMeasurement = "", Icon = "alert-outline" },
        new RegisterTopic { Name = "BattPower", UnitOfMeasurement = "W", Icon = "car-battery", DeviceClass="power", StateClass="measurement" },
        new RegisterTopic { Name = "BattCurrent", UnitOfMeasurement = "A", Icon = "current-dc", DeviceClass="current", StateClass="measurement" },
        new RegisterTopic { Name = "BattVoltageGrade", UnitOfMeasurement = "V", Icon = "current-dc", DeviceClass="voltage", StateClass="measurement" },
        new RegisterTopic { Name = "RatedPowerW", UnitOfMeasurement = "W", Icon = "certificate", DeviceClass="power", StateClass="measurement" },
        new RegisterTopic { Name = "CommunicationProtocalEdition", UnitOfMeasurement = "", Icon = "barcode" },
        new RegisterTopic { Name = "ArrowFlag", UnitOfMeasurement = "", Icon = "state-machine" },
        new RegisterTopic { Name = "ChrWorkstateNo", UnitOfMeasurement = "", Icon = "state-machine" },
        new RegisterTopic { Name = "MpptStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "ChargingStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "PvVoltage", UnitOfMeasurement = "V", Icon = "current-dc", DeviceClass="voltage", StateClass="measurement"  },
        new RegisterTopic { Name = "ChrBatteryVoltage", UnitOfMeasurement = "V", Icon = "current-dc", DeviceClass="voltage", StateClass="measurement"  },
        new RegisterTopic { Name = "ChargerCurrent", UnitOfMeasurement = "A", Icon = "current-dc", DeviceClass="current", StateClass="measurement"  },
        new RegisterTopic { Name = "ChargerPower", UnitOfMeasurement = "W", Icon = "car-turbocharger", DeviceClass="power", StateClass="measurement" },
        new RegisterTopic { Name = "RadiatorTemp", UnitOfMeasurement = "°C", Icon = "thermometer", DeviceClass="temperature", StateClass="measurement" },
        new RegisterTopic { Name = "ExternalTemp", UnitOfMeasurement = "°C", Icon = "thermometer", DeviceClass="temperature", StateClass="measurement" },
        new RegisterTopic { Name = "BatteryRelayNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "PvRelayNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "ChrError1", UnitOfMeasurement = "", Icon = "alert-circle-outline" },
        new RegisterTopic { Name = "ChrWarning1", UnitOfMeasurement = "", Icon = "alert-outline" },
        new RegisterTopic { Name = "BattVolGrade", UnitOfMeasurement = "V", Icon = "current-dc", DeviceClass="voltage", StateClass="measurement" },
        new RegisterTopic { Name = "RatedCurrent", UnitOfMeasurement = "A", Icon = "current-dc", DeviceClass="current", StateClass="measurement" },
        new RegisterTopic { Name = "AccumulatedDay", UnitOfMeasurement = "day", Icon = "calendar-today" },
        new RegisterTopic { Name = "AccumulatedHour", UnitOfMeasurement = "hour", Icon = "clock-outline" },
        new RegisterTopic { Name = "AccumulatedMinute", UnitOfMeasurement = "min", Icon = "timer-outline" },
        new RegisterTopic { Name = "BatteryPercent", UnitOfMeasurement = "%", Icon = "battery" },
        new RegisterTopic { Name = "AccumulatedChargerPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative", DeviceClass = "energy", StateClass = "total_increasing" },
        new RegisterTopic { Name = "AccumulatedDischargerPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative", DeviceClass = "energy", StateClass = "total_increasing" },
        new RegisterTopic { Name = "AccumulatedBuyPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative", DeviceClass = "energy", StateClass = "total_increasing" },
        new RegisterTopic { Name = "AccumulatedSellPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedLoadPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative", DeviceClass = "energy", StateClass = "total_increasing" },
        new RegisterTopic { Name = "AccumulatedSelfusePower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative", DeviceClass = "energy", StateClass = "total_increasing" },
        new RegisterTopic { Name = "AccumulatedPvsellPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedGridChargerPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedPvPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" }
    };

    public static readonly JsonSerializerOptions SerializeOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

    public static List<PublishPayload> JsonToPublishList(string json)
    {
        List<PublishPayload> results = new();
        EP1800? model = JsonSerializer.Deserialize<EP1800>(json);
        if (model == null)
        {
            return results;
        }

        foreach (var prop in model.GetType().GetProperties())
        {
            var matches = Topics.Any(p => p.Name == prop.Name);
            if (matches)
            {
                results.Add(new PublishPayload { Topic = prop.Name, Value = prop.GetValue(model)?.ToString() ?? "" });
            }
        }
        return results;
    }
}
