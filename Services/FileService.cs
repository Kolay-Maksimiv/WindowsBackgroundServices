using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Services;

public class FileService : IFileService
{

    public void CreateExcelFile(object? state)
    {
        string folderPath = "C:\\Users\\MykolaMaksymiv\\OneDrive - Netfully Ltd\\Desktop\\Files";
        string filePath = Path.Combine(folderPath, $"File {DateTime.Now.ToString("dddd, MMMM dd yyyy hh-mm")}.xlsx");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
        {
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

    public void ParseExcelFile(object? state)
    {
        string folderPath = "C:\\Users\\MykolaMaksymiv\\OneDrive - Netfully Ltd\\Desktop\\Files";

        if (Directory.Exists(folderPath))
        {
            foreach (string filePath in Directory.GetFiles(folderPath, "*.xlsx"))
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart workbookPart = document.WorkbookPart;
                    Sheet sheet = workbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
                    if (sheetData != null)
                    {
                        foreach (Row row in sheetData.Elements<Row>())
                        {
                            foreach (Cell cell in row.Elements<Cell>())
                            {
                                Console.Write(cell.InnerText + " ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}

public interface IFileService
{
    public void CreateExcelFile(object? state);
    public void ParseExcelFile(object? state);
}