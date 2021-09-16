using Microsoft.AspNetCore.Http;

namespace Session.Accessor.Service.Contracts
{
    public interface ISessionInfoExtractor
    {
        /// <summary>
        /// Extract the UserId from the authorization data.
        /// Extract the ClientId form the authorization data.
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        ExecutorInfoDTO ExtractExecutorInformation(HttpContext header);
    }
}