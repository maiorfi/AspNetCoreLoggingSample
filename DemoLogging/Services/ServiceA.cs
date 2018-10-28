using Microsoft.Extensions.Logging;

namespace DemoLogging.Services
{
    public class ServiceA
    {
        private readonly ILogger Logger;

        public ServiceA(ILogger<ServiceA> logger)
        {
            Logger = logger;

            Logger.LogTrace("ServiceA CTOR called");
        }

        public string GetSomething(string what)
        {
            Logger.LogTrace("GetSomething() chiamato con {COSA}", what);

            return what.ToUpperInvariant();
        }
    }
}