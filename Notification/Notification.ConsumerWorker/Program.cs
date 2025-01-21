using Notification.ConsumerWorker;
using Notification.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddInfrastructureService(builder.Configuration);

var host = builder.Build();
host.Run();
