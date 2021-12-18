using System.Collections.Generic;
using BlobStorage.Accessor.Contracts.DTOs;

namespace BlobStorage.Accessor.Contracts.Commands.UploadContent;

public class UploadItemCommand : WebPath
{
    public string Stream { get; set; }
        
    public string ContentType { get; set; } = "Image";

    public string Extension { get; set; } = ".png";
        
    public Dictionary<string,string> Tags { get; set; }
}