using Microsoft.Extensions.Options;
using Quartz;

namespace Must;

public class PollJob : IJob
{
    private readonly ILogger<PollJob> _logger;
    private readonly Config _config;
    private readonly Poller _poller;
    private readonly Tester _tester;
    public PollJob(ILogger<PollJob> logger, IOptions<Config> config, Poller poller, Tester tester)
    {
        _logger = logger;
        _config = config.Value;
        _poller = poller;
        _tester = tester;
    }

    public Task Execute(IJobExecutionContext context)
    {
        if (_config.IsTest)
        {
            _logger.LogInformation("Starting test on port {PortName}", _config.PortName);
            _tester.Test();
            _logger.LogInformation("===== Finished Test =====");
        }
        else
        {
            _logger.LogInformation("Result! {json}", _poller.GetJSON());
        }

        return Task.CompletedTask;
    }
}