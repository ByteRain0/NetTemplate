using Hangfire;
using Newtonsoft.Json;

namespace MessageDispatchingEngine.Service.Infrastructure
{
    public static class HangFireConfigurationExtensions
    {
        public static void UseMediatR(this IGlobalConfiguration configuration)
        {
            var jsonSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            configuration.UseSerializerSettings(jsonSettings);
        }
    }
}