using Chuck.Application;
using Chuck.Infrastructure;
using Chuck.Worker;
using Chuck.Worker.Http;
using Chuck.Worker.Services;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((cnt, services ) =>
    {
        services.Configure<WorkerConfig>(cnt.Configuration.GetSection("WorkerConfiguration"));
        services.Configure<QuotesDbSettings>(cnt.Configuration.GetSection("MongoConfiguration"));
        services.AddScoped<IHarvestQuotesService, HarvestQuotesService>();
        services.AddHttpClient<IChuckHttpClient, ChuckHttpClient>();
        services.AddInfrastructureModule();
        services.AddApplicationModule();
        services.AddHostedService<Worker>();
    }).UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration))
    .Build();

await host.RunAsync();
