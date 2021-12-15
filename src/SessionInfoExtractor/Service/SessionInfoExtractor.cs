using Microsoft.AspNetCore.Http;
using Session.Accessor.Service.Contracts;

namespace Session.Accessor.Service.Service;

public class SessionInfoExtractor : ISessionInfoExtractor
{
    public ExecutorInfoDTO ExtractExecutorInformation(HttpContext header)
    {
        return new ExecutorInfoDTO()
        {
            Id = "f73ea907-2027-48c6-8af6-52c6c6d9d22d",
            Name = "Vasilii Oleinic"
        };
    }
}