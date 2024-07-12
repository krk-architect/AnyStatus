using System.ComponentModel;
using System.Management;
using AnyStatus.API.Widgets;

#pragma warning disable CA1416 // The call site is reachable on all platforms

namespace AnyStatus.Plugins.SystemInformation;

[Category("System Information")]
[DisplayName("CPU Temperature")]
[Description("The current CPU temperature")]
public class CpuTemperatureWidget : MetricWidget, IPollable, ICommonWidget
{
    private TemperatureScale _scale;
    private string           _suffix;

    public CpuTemperatureWidget()
    {
        MinValue = 0;
        MaxValue = 100;
    }

    public TemperatureScale Scale
    {
        get => _scale;
        set
        {
            _scale  = value;
            _suffix = _scale == TemperatureScale.Celsius ? "°C" : "°F";
        }
    }

    public override string ToString() => $"{Value:N1} {_suffix}";
}

public enum TemperatureScale
{
    Celsius
  , Fahrenheit
}

public class CpuTemperatureQuery : MetricQuery<CpuTemperatureWidget>
{
    protected override void Handle(MetricRequest<CpuTemperatureWidget> request)
    {
        if (TryGetCpuTemperature(request.Context.Scale, out var temperature))
        {
            request.Context.Value  = temperature;
            request.Context.Status = Status.OK;
        }
        else
        {
            request.Context.Status = Status.Unknown;
        }
    }

    private static bool TryGetCpuTemperature(TemperatureScale scale, out double temperature)
    {
        var query    = new SelectQuery("""
                                       SELECT *
                                       FROM Win32_PerfFormattedData_Counters_ThermalZoneInformation
                                       """);

        var searcher = new ManagementObjectSearcher(query);
        var results  = searcher.Get();

        if (results.Count == 0)
        {
            temperature = double.NaN;

            return false;
        }

        foreach (var item in results)
        {
            var value = item.GetPropertyValue("HighPrecisionTemperature");

            if (value is not uint temp)
            {
                continue;
            }

            temperature = (double)temp / 10 - 273.15;  // Kelvin to Celsius

            if (scale == TemperatureScale.Celsius)
            {
                return true;
            }

            temperature = 9.0 / 5 * temperature + 32; // Celsius to Fahrenheit

            return true;
        }

        temperature = double.NaN;

        return false;
    }
}