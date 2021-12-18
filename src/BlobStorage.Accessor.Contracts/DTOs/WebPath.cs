namespace BlobStorage.Accessor.Contracts.DTOs;

public abstract class WebPath
{
    public string RelativePath { get; set; }

    public string FileName { get; set; }
        
    public string GetFilePath => $"{RelativePath}/{FileName}";
}