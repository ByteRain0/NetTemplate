namespace BlobStorage.Accessor.Contracts.Queries;

public class DownloadContentQuery
{
    public string FileName { get; set; }
        
    public string Path { get; set; }

    public string GetSingleFilePath => $"{Path}/{FileName}";
}