using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands.UploadContent;
using BlobStorage.Accessor.Contracts.Queries.ListItems;
using BlobStorage.Accessor.Tests.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlobStorage.Accessor.Tests.Tests.Commands;

[TestClass]
public class UploadCommandTests : TestBase
{
        
    [ClassInitialize]
    public static void Initialization(TestContext context)
    {
        InitialSetup(context);
    }
        
    [TestMethod]
    public async Task UploadShouldWorkAndUploadOnlyOneFile()
    {
        // Arrange
        var storageAccessor = GetService<IStorageAccessor>();
        var configurations = GetService<IOptions<BlobStorageTestConfigurations>>().Value;
        var testId = $"{nameof(UploadShouldWorkAndUploadOnlyOneFile)}-{Guid.NewGuid()}";
        var payload = new UploadItemCommand()
        {
            RelativePath = configurations.TestsFolderPath,
            Stream = configurations.SampleImageStream,
            Tags = new Dictionary<string, string>()
            {
                {
                    nameof(UploadShouldWorkAndUploadOnlyOneFile), testId.ToString()
                }
            },
            FileName = testId.ToString()
        };

        // Act
        Console.WriteLine($"Running test with ID : {testId}");
        var operation = await storageAccessor.Upload(request: payload, cancellationToken: CancellationToken.None);
        var itemExists = await storageAccessor.ListItems(
            request: new ListFilesQuery()
            {
                RelativePath = configurations.TestsFolderPath
            }, cancellationToken: CancellationToken.None);

        // Assert
        operation.IsSuccess.Should().BeTrue();
        itemExists.Value.Count(x => x.ToLowerInvariant().Contains(testId.ToString().ToLowerInvariant())).Should()
            .Be(1);
    }
}