using Microsoft.Extensions.Options;
using Quartz;

namespace Must;

public class PollJob : IJob
{
    private readonly ILogger<PollJob> _logger;
    private readonly Config _config;
    private readonly Poller _poller;
    public PollJob(ILogger<PollJob> logger, IOptions<Config> config, Poller poller)
    {
        _logger = logger;
        _config = config.Value;
        _poller = poller;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Hello world! {PortName}", _config.PortName);
        _logger.LogInformation("Result! {json}", _poller.GetJSON());
        return Task.CompletedTask;
    }
}