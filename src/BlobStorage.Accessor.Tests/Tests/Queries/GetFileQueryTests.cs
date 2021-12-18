using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands.UploadContent;
using BlobStorage.Accessor.Contracts.Queries.DownloadContent;
using BlobStorage.Accessor.Tests.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlobStorage.Accessor.Tests.Tests.Queries;

[TestClass]
public class GetFileQueryTests : TestBase
{
        
    [ClassInitialize]
    public static void Initialization(TestContext context)
    {
        InitialSetup(context);
    }
        
    [TestMethod]
    public async Task DownloadFileShouldWork()
    {
        // Arrange
        var storageAccessor = GetService<IStorageAccessor>();
        var configurations = GetService<IOptions<BlobStorageTestConfigurations>>().Value;
        var testId = $"{nameof(DownloadFileShouldWork)}-{Guid.NewGuid()}";
        var payload = new UploadItemCommand()
        {
            RelativePath = configurations.TestsFolderPath,
            Stream = configurations.SampleImageStream,
            Tags = new Dictionary<string, string>()
            {
                {
                    nameof(DownloadFileShouldWork), testId.ToString()
                }
            },
            FileName = testId.ToString()
        };
        await storageAccessor.Upload(request: payload, cancellationToken: CancellationToken.None);

        // Act
        Console.WriteLine($"Running test with ID : {testId}");
        var operation = await storageAccessor.Download(
            request: new DownloadItemQuery()
            {
                Path = configurations.TestsFolderPath,
                FileName = testId.ToString()
            }, cancellationToken: CancellationToken.None);
            
        // Assert
        operation.IsSuccess.Should().BeTrue();
        operation.Value.Should().NotBeNullOrEmpty();
    }
}