using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TimerTrigger1.Data;
using TimerTrigger1.Services;

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
        s.AddScoped<IArticleService, ArticleService>();
    })
    .Build();

host.Run();