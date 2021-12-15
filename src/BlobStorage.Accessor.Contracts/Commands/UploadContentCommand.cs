using System.Collections.Generic;

namespace BlobStorage.Accessor.Contracts.Commands;

public class UploadContentCommand
{
    public string Stream { get; set; }
    public string CustomPath { get; set; }

    public string ContentType { get; set; }

    public string Extension { get; set; }

    public string FileName { get; set; }

    public Dictionary<string,string> Tags { get; set; }
}