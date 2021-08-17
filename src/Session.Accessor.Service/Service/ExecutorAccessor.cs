using Microsoft.AspNetCore.Http;
using Session.Accessor.Service.Contracts;

namespace Session.Accessor.Service.Service
{
    /// <summary>
    /// TODO: leave this service as an Interface programming model example.
    /// TODO: add a decorator with Scrutor that will catch all exceptions and return Response.
    /// </summary>
    public class ExecutorAccessor : IExecutorAccessor
    {
        public string GetExecutorId(HttpContext header)
        {
            return "f73ea907-2027-48c6-8af6-52c6c6d9d22d";
        }

        public string GetExecutorName(HttpContext header)
        {
            return "Vasilii Oleinic";
        }
    }
}