using CreatorFiles.Extensions;
using Services;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
     {
         options.ServiceName = "Creator Files Service";
     })
    .ConfigureServices(services =>
    {
        services.AddHostedService<ExcelCreationService>();
        services.AddDatabase();
        services.AddEntityRepositories();
        services.AddUnitOfWork();
        services.AddSingleton<IFileService, FileService>();

    })
    .Build();


await host.RunAsync();
