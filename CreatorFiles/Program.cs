using CreatorFiles;
using CreatorFiles.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
     {
       options.ServiceName = "Creator Files Service";
     })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddDatabase();
        services.AddEntityRepositories();
        services.AddUnitOfWork();

    })
    .Build();


await host.RunAsync();
