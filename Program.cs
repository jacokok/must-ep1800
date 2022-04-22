using Must;
using Serilog;

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Logger = log;

var builder = ScheduleHostBuilder.CreateHostBuilder(args);
var app = builder.Build();
app.Run();