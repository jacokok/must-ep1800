using Must;

var builder = ScheduleHostBuilder.CreateHostBuilder(args);
var app = builder.Build();
app.Run();