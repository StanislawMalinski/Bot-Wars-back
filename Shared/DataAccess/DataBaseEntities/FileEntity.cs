namespace Shared.DataAccess.DataBaseEntities;

public class FileEntity
{
    public int FileId { get; set; }

    public string? FilePath { get; set; }
    
    public byte[]? FileContent { get; set; }
}