using CreatorFiles;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
     {
       options.ServiceName = "Creator Files Service";
     })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
