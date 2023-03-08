using Data.Enums;

namespace Data.Entities;

public class FileInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreateData { get; set; }
    public DateTime UpdateData { get; set; }
    public StatusFile StatusFile { get; set; }
    public virtual ICollection<FileData> FileData { get; set; }
}
