using Must.Extensions;
using Quartz;
using Serilog;

namespace Must;

public static class ScheduleHostBuilder
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                Config config = new();
                configuration.GetSection("Config").Bind(config);
                services.Configure<Config>(configuration.GetSection("Config"));

                services.ConfigureMQTTClient(configuration);
                services.AddHostedService<StartupHostedService>();

                services.AddQuartz(q =>
                {
                    if (config.IsTest)
                    {
                        q.ScheduleJob<PollJob>(trigger => trigger
                            .WithIdentity("Test")
                            .StartNow());
                    }
                    else
                    {
                        q.ScheduleJob<PollJob>(trigger => trigger
                            .WithIdentity("PollStart")
                            .StartNow());

                        q.ScheduleJob<PollJob>(trigger => trigger
                            .WithIdentity("Poll")
                            .ForJob("Poll")
                            .WithCronSchedule(config.Cron)
                            .StartNow());
                    }

                    q.UseMicrosoftDependencyInjectionJobFactory();
                });

                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                services.AddSingleton<PollJob>();
                services.AddSingleton<Poller>();
                services.AddSingleton<Tester>();
            })
            .ConfigureAppConfiguration(configHost => configHost.AddEnvironmentVariables(prefix: "MUST_"));
}
