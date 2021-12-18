using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands.DeleteItem;
using BlobStorage.Accessor.Contracts.Commands.UploadContent;
using BlobStorage.Accessor.Contracts.Queries.ListItems;
using BlobStorage.Accessor.Tests.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlobStorage.Accessor.Tests.Tests.Commands;

[TestClass]
public class DeleteFileCommandTests : TestBase
{
    [ClassInitialize]
    public static void Initialization(TestContext context)
    {
        InitialSetup(context);
    }
        
    [TestMethod]
    public async Task DeleteFileShouldWork()
    {
        // Arrange
        var storageAccessor = GetService<IStorageAccessor>();
        var configurations = GetService<IOptions<BlobStorageTestConfigurations>>().Value;
        var testId = $"{nameof(DeleteFileShouldWork)}-{Guid.NewGuid()}";
        var payload = new UploadItemCommand()
        {
            RelativePath = configurations.TestsFolderPath,
            Stream = configurations.SampleImageStream,
            Tags = new Dictionary<string, string>()
            {
                {
                    nameof(DeleteFileShouldWork), testId.ToString()
                }
            },
            FileName = testId.ToString()
        };
        await storageAccessor.Upload(request: payload, cancellationToken: CancellationToken.None);


        // Act
        Console.WriteLine($"Running test with ID : '{testId}'");
        var operation = await storageAccessor.DeleteFile(
            request: new DeleteItemCommand()
            {
                RelativePath = configurations.TestsFolderPath,
                FileName = testId.ToString()
            }, cancellationToken: CancellationToken.None);

        var itemExists = await storageAccessor.ListItems(
            request: new ListFilesQuery()
            {
                RelativePath = configurations.TestsFolderPath
            }, cancellationToken: CancellationToken.None);

        // Assert
        operation.IsSuccess.Should().BeTrue();
        itemExists.Value.Any(x => x.ToLowerInvariant().Contains(testId.ToString())).Should().BeFalse();
    }
}