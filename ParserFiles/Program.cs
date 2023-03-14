using ParserFiles;
using ParserFiles.Extensions;
using Services;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "Parser Files Service";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<ExcelParserService>();
        services.AddDatabase();
        services.AddEntityRepositories();
        services.AddUnitOfWork();
        services.AddSingleton<IFileService, FileService>();

    })
    .Build();


await host.RunAsync();
