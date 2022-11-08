using SubscriptionActions.Data;
using SubscriptionActions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddJsonFile("local.settings.json", optional: true,
            reloadOnChange: true);
    })
    .ConfigureServices(s =>
    {
        s.AddDbContext<AppDbContext>();
        s.AddScoped<ISubscriptionService, SubscriptionService>();
    })

    .Build();

host.Run();