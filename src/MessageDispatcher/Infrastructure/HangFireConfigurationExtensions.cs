using Hangfire;
using Newtonsoft.Json;

namespace MessageDispatcher.Infrastructure
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