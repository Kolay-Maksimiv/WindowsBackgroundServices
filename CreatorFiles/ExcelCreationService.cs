using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Services;

public class ExcelCreationService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IFileService _fileService;
    public ExcelCreationService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(_fileService.CreateExcelFile, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
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
