namespace BlobStorage.Accessor.Contracts.Queries.DownloadContent;

public class DownloadItemQuery
{
    public string FileName { get; set; }
        
    public string Path { get; set; }

    public string GetSingleFilePath => $"{Path}/{FileName}";
}