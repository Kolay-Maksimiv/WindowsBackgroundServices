namespace Data.Entities;

public class FileData
{
    public int Id { get; set; }
    public Guid FileInfoId { get; set; }
    public string Segment { get; set; }
    public string Country { get; set; }
    public string Product { get; set; }
    public bool Status { get; set; }
    public int UnitsSold { get; set; }
    public double SalePrice { get; set; }
    public FileInfo FileInfo { get; set; }
}
