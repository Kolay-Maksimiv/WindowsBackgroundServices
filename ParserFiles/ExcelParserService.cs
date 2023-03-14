using Services;

namespace ParserFiles;

public class ExcelParserService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IFileService _fileService;
    public ExcelParserService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(_fileService.ParseExcelFile, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Dispose();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}

