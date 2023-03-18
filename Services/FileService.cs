using Data;
using Data.Entities;
using Data.Enums;
using Data.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Services;

public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;

    public FileService(ILogger<FileService> logger)
    {
        _logger = logger;
    }

    public void CreateExcelFile(object? state)
    {
        _logger.LogInformation($"Create excel file started {DateTime.Now:dd/MM/yyyy hh:mm}");

        string folderPath = "C:\\Users\\MykolaMaksymiv\\OneDrive - Netfully Ltd\\Desktop\\Files";
        string filePath = Path.Combine(folderPath, $"File {DateTime.Now:dddd, MMMM dd yyyy hh-mm}.xlsx");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            _logger.LogInformation($"Create directory {DateTime.Now:dd/MM/yyyy hh:mm}");
        }

        CreateFile(filePath);

        SaveFileInfo(filePath);

        _logger.LogInformation($"Finish create excel file sevice {DateTime.Now:dd/MM/yyyy hh:mm}");
    }

    public async void ParseExcelFile(object? state)
    {

        _logger.LogInformation($"Parser excel file started {DateTime.Now:dd/MM/yyyy hh:mm}");

        using ApplicationDbContext dbContext = new ApplicationDbContext();

        var datasForDb = await dbContext.FileInfos.ToListAsync();

        string folderPath = "C:\\Users\\MykolaMaksymiv\\OneDrive - Netfully Ltd\\Desktop\\Files";

        if (Directory.Exists(folderPath))
        {
            foreach (string filePath in Directory.GetFiles(folderPath, "*.xlsx"))
            {
                var fileInfo = new System.IO.FileInfo(filePath);

                if (datasForDb.Any(x => x.Name == fileInfo.Name && x.StatusFile == StatusFile.NoRead))
                {
                    _logger.LogInformation($"Parse file {fileInfo.Name} | {DateTime.Now:dd/MM/yyyy hh:mm}");

                    var data = ParseFile(filePath);

                    _logger.LogInformation($"Save data file to database {fileInfo.Name} | {DateTime.Now:dd/MM/yyyy hh:mm}");

                    var file = datasForDb.Where(x => x.Name == fileInfo.Name).FirstOrDefault();

                    SaveFileData(data, file.Id);

                    file.StatusFile = StatusFile.Read;
                    file.UpdateData = DateTime.Now;

                    dbContext.FileInfos.Update(file);

                    dbContext.SaveChanges();
                }
            }
        }

        _logger.LogInformation($"Finish parser excel files sevice {DateTime.Now:dd/MM/yyyy hh:mm}");
    }

    private List<DataModel> ParseFile(string filePath)
    {

        var dataList = new List<DataModel>();

        using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            Sheet sheet = workbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
            if (sheetData != null)
            {
                int i = 0;
                foreach (Row row in sheetData.Elements<Row>())
                {
                    if (i > 1)
                    {
                        var dataModel = new DataModel();
                        int j = 1;
                        foreach (Cell cell in row.Elements<Cell>())
                        {
                            string cellValue = cell.InnerText;
                            if (cellValue != null)
                            {
                                switch (j)
                                {
                                    case 1:
                                        dataModel.Segment = cellValue;
                                        break;
                                    case 2:
                                        dataModel.Country = cellValue;
                                        break;
                                    case 3:
                                        dataModel.Product = cellValue;
                                        break;
                                    case 4:
                                        dataModel.SalePrice = Convert.ToDouble(cellValue);
                                        break;
                                }
                            }
                            j++;
                        }
                        dataList.Add(dataModel);
                    }
                    i++;
                }

            }
        }

        return dataList;
    }

    private static void SaveFileData(List<DataModel> dataList, Guid fileInfoId)
    {

        using (ApplicationDbContext dbContext = new ApplicationDbContext())
        {
            foreach (var data in dataList)
            {
                var fileData = new FileData
                {
                    Segment = data.Segment,
                    Country = data.Country,
                    Product = data.Product,
                    SalePrice = data.SalePrice,
                    FileInfoId = fileInfoId
                };

                dbContext.FileDatas.Add(fileData);
            }

            dbContext.SaveChanges();
        }
    }

    private void SaveFileInfo(string filePath)
    {
        _logger.LogInformation($"Save file info to database {DateTime.Now:dd/MM/yyyy hh:mm}");
        var fileInfo = new System.IO.FileInfo(filePath);

        using (ApplicationDbContext dbContext = new ApplicationDbContext())
        {
            dbContext.FileInfos.Add(new Data.Entities.FileInfo
            {
                Name = fileInfo.Name,
                CreateData = DateTime.Now,
                UpdateData = DateTime.Now,
                StatusFile = StatusFile.NoRead
            });

            dbContext.SaveChanges();
        }
    }

    private void CreateFile(string filePath)
    {
        _logger.LogInformation($"Create excel file  {DateTime.Now:dd/MM/yyyy hh:mm}");
        using SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

        WorkbookPart workbookPart = document.AddWorkbookPart();
        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        SheetData sheetData = new SheetData();
        worksheetPart.Worksheet = new Worksheet(sheetData);
        Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
        workbookPart.Workbook = new Workbook();
        workbookPart.Workbook.Sheets = new Sheets();
        workbookPart.Workbook.Sheets.Append(sheet);

        Row row1 = new Row();
        Cell cellA1 = new Cell() { DataType = CellValues.String, CellValue = new CellValue("Segment") };
        Cell cellB1 = new Cell() { DataType = CellValues.String, CellValue = new CellValue("Country") };
        Cell cellC1 = new Cell() { DataType = CellValues.String, CellValue = new CellValue("Product") };
        Cell cellD1 = new Cell() { DataType = CellValues.Number, CellValue = new CellValue("UnitsSold") };
        Cell cellE1 = new Cell() { DataType = CellValues.Number, CellValue = new CellValue("SalePrice") };

        row1.Append(cellA1);
        row1.Append(cellB1);
        row1.Append(cellC1);
        row1.Append(cellD1);
        row1.Append(cellE1);

        sheetData.Append(row1);

        workbookPart.Workbook.Save();
    }
}

public interface IFileService
{
    public void CreateExcelFile(object? state);
    public void ParseExcelFile(object? state);
}