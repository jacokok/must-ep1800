using Quartz;
using Serilog;
using YamlDotNet.Serialization.NamingConventions;

namespace Must;

public static class ScheduleHostBuilder
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureServices((_, services) =>
            {
                var config = GetYamlConfig();
                services.AddSingleton(config);

                services.AddQuartz(q =>
                {
                    q.AddJob<PollJob>(opts => opts.WithIdentity("Poll"));

                    q.AddTrigger(opts => opts
                        .ForJob("Poll")
                        .WithIdentity("PollTrigger")
                        .WithCronSchedule(config.Cron));

                    q.UseMicrosoftDependencyInjectionJobFactory();
                });

                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                services.AddSingleton<PollJob>();
                services.AddSingleton<Poller>();
            });

    public static Config GetYamlConfig()
    {
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
        return deserializer.Deserialize<Config>(File.ReadAllText("config.yml"));
    }
}
