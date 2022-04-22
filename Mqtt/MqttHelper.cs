using System.Text.Json;
using Must.Models;

namespace Must.Mqtt;
public static class MqttHelper
{
    public static readonly List<RegisterTopic> Topics = new() {
        new RegisterTopic { Name = "WorkStateNo", UnitOfMeasurement = "", Icon = "state-machine" },
        new RegisterTopic { Name = "AcVoltageGrade", UnitOfMeasurement = "Vac", Icon = "current-ac" },
        new RegisterTopic { Name = "RatedPower", UnitOfMeasurement = "VA", Icon = "lightbulb-outline" },
        new RegisterTopic { Name = "BatteryVoltage", UnitOfMeasurement = "Vdc-batt", Icon = "current-dc" },
        new RegisterTopic { Name = "InverterVoltage", UnitOfMeasurement = "Vac", Icon = "current-ac" },
        new RegisterTopic { Name = "GridVoltage", UnitOfMeasurement = "Vac", Icon = "current-ac" },
        new RegisterTopic { Name = "BusVoltage", UnitOfMeasurement = "Vdc/Vac", Icon = "cog-transfer-outline" },
        new RegisterTopic { Name = "ControlCurrent", UnitOfMeasurement = "Aac", Icon = "current-ac" },
        new RegisterTopic { Name = "InverterCurrent", UnitOfMeasurement = "Aac", Icon = "current-ac" },
        new RegisterTopic { Name = "GridCurrent", UnitOfMeasurement = "Aac", Icon = "current-ac" },
        new RegisterTopic { Name = "LoadCurrent", UnitOfMeasurement = "Aac", Icon = "current-ac" },
        new RegisterTopic { Name = "PInverter", UnitOfMeasurement = "W", Icon = "cog-transfer-outline" },
        new RegisterTopic { Name = "PGrid", UnitOfMeasurement = "W", Icon = "transmission-tower" },
        new RegisterTopic { Name = "PLoad", UnitOfMeasurement = "W", Icon = "lightbulb-on-outline" },
        new RegisterTopic { Name = "LoadPercent", UnitOfMeasurement = "%", Icon = "progress-download" },
        new RegisterTopic { Name = "SInverter", UnitOfMeasurement = "VA", Icon = "cog-transfer-outline" },
        new RegisterTopic { Name = "SGrid", UnitOfMeasurement = "VA", Icon = "transmission-tower" },
        new RegisterTopic { Name = "SLoad", UnitOfMeasurement = "VA", Icon = "lightbulb-on-outline" },
        new RegisterTopic { Name = "QInverter", UnitOfMeasurement = "Var", Icon = "cog-transfer-outline" },
        new RegisterTopic { Name = "QGrid", UnitOfMeasurement = "Var", Icon = "transmission-tower" },
        new RegisterTopic { Name = "QLoad", UnitOfMeasurement = "Var", Icon = "lightbulb-on-outline" },
        new RegisterTopic { Name = "InverterFrequency", UnitOfMeasurement = "Hz", Icon = "sine-wave" },
        new RegisterTopic { Name = "GridFrequency", UnitOfMeasurement = "Hz", Icon = "sine-wave" },
        new RegisterTopic { Name = "InverterMaxNumber", UnitOfMeasurement = "", Icon = "format-list-numbered" },
        new RegisterTopic { Name = "CombineType", UnitOfMeasurement = "", Icon = "format-list-bulleted-type" },
        new RegisterTopic { Name = "InverterNumber", UnitOfMeasurement = "", Icon = "format-list-numbered" },
        new RegisterTopic { Name = "AcRadiatorTemp", UnitOfMeasurement = "oC", Icon = "thermometer" },
        new RegisterTopic { Name = "TransformerTemp", UnitOfMeasurement = "oC", Icon = "thermometer" },
        new RegisterTopic { Name = "DcRadiatorTemp", UnitOfMeasurement = "oC", Icon = "thermometer" },
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
        new RegisterTopic { Name = "BattPower", UnitOfMeasurement = "W", Icon = "car-battery" },
        new RegisterTopic { Name = "BattCurrent", UnitOfMeasurement = "Adc", Icon = "current-dc" },
        new RegisterTopic { Name = "BattVoltageGrade", UnitOfMeasurement = "Vdc-batt", Icon = "current-dc" },
        new RegisterTopic { Name = "RatedPowerW", UnitOfMeasurement = "W", Icon = "certificate" },
        new RegisterTopic { Name = "CommunicationProtocalEdition", UnitOfMeasurement = "", Icon = "barcode" },
        new RegisterTopic { Name = "ArrowFlag", UnitOfMeasurement = "", Icon = "state-machine" },
        new RegisterTopic { Name = "ChrWorkstateNo", UnitOfMeasurement = "", Icon = "state-machine" },
        new RegisterTopic { Name = "MpptStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "ChargingStateNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "PvVoltage", UnitOfMeasurement = "Vdc-pv", Icon = "current-dc" },
        new RegisterTopic { Name = "ChrBatteryVoltage", UnitOfMeasurement = "Vdc-batt", Icon = "current-dc" },
        new RegisterTopic { Name = "ChargerCurrent", UnitOfMeasurement = "Adc", Icon = "current-dc" },
        new RegisterTopic { Name = "ChargerPower", UnitOfMeasurement = "W", Icon = "car-turbocharger" },
        new RegisterTopic { Name = "RadiatorTemp", UnitOfMeasurement = "oC", Icon = "thermometer" },
        new RegisterTopic { Name = "ExternalTemp", UnitOfMeasurement = "oC", Icon = "thermometer" },
        new RegisterTopic { Name = "BatteryRelayNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "PvRelayNo", UnitOfMeasurement = "", Icon = "electric-switch" },
        new RegisterTopic { Name = "ChrError1", UnitOfMeasurement = "", Icon = "alert-circle-outline" },
        new RegisterTopic { Name = "ChrWarning1", UnitOfMeasurement = "", Icon = "alert-outline" },
        new RegisterTopic { Name = "BattVolGrade", UnitOfMeasurement = "Vdc-batt", Icon = "current-dc" },
        new RegisterTopic { Name = "RatedCurrent", UnitOfMeasurement = "Adc", Icon = "current-dc" },
        new RegisterTopic { Name = "AccumulatedDay", UnitOfMeasurement = "day", Icon = "calendar-today" },
        new RegisterTopic { Name = "AccumulatedHour", UnitOfMeasurement = "hour", Icon = "clock-outline" },
        new RegisterTopic { Name = "AccumulatedMinute", UnitOfMeasurement = "min", Icon = "timer-outline" },
        new RegisterTopic { Name = "BatteryPercent", UnitOfMeasurement = "%", Icon = "battery" },
        new RegisterTopic { Name = "AccumulatedChargerPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedDischargerPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedBuyPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedSellPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedLoadPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedSelfusePower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedPvsellPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedGridChargerPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" },
        new RegisterTopic { Name = "AccumulatedPvPower", UnitOfMeasurement = "kWh", Icon = "chart-bell-curve-cumulative" }
    };

    public static readonly JsonSerializerOptions SerializeOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
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