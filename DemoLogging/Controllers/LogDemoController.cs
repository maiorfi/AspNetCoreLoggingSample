using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoLogging.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoLogging.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Produces("application/json")]
    public class LogDemoController : ControllerBase
    {
        private readonly ILogger Logger;
        private readonly ServiceA ServiceAInstance;

        public LogDemoController(ILogger<LogDemoController> logger, ServiceA serviceA)
        {
            Logger = logger;
            ServiceAInstance = serviceA;
        }

        [HttpPost]
        [Route("logtrace")]
        public ActionResult<string> LogTrace([FromBody] string message)
        {
            using (Logger.BeginScope($"Scope {message}-{DateTime.Now.Ticks.ToString().Substring(9)}"))
            {
                Logger.LogTrace(new EventId(LoggingEvents.EventID_Method_LogTrace, LoggingEvents.EventID_Method_LogTrace_Name), "LogTrace chiamato con parametro {MESSAGGIO}", message);

                var retval = ServiceAInstance.GetSomething(message);

                return Ok(retval);
            }
        }

        [HttpPost]
        [Route("raiseexception")]
        public ActionResult RaiseException([FromBody] string arg)
        {
            using (Logger.BeginScope($"Scope exception-demo-{DateTime.Now.Ticks.ToString().Substring(9)}"))
            {
                try
                {
                    throw new ArgumentException("Eccezione generata volutamente", nameof(arg));
                }
                catch (Exception ex)
                {
                    Logger.LogError(new EventId(LoggingEvents.EventID_Method_RaiseException, LoggingEvents.EventID_Method_RaiseException_Name), ex, $"ECCEZIONE QUANDO ARG ERA '{arg}'");

                    return NoContent();
                }
            }
        }
    }

    public class LoggingEvents
    {
        public const int EventID_Generic = 1000;
        public const string EventID_Generic_Name = "EVT_ID_GENERIC";

        public const int EventID_Method_LogTrace = 2000;
        public const string EventID_Method_LogTrace_Name = "EVT_ID_METHOD_LOGTRACE";

        public const int EventID_Method_RaiseException = 2001;
        public const string EventID_Method_RaiseException_Name = "EVT_ID_METHOD_RAISEEXCEPTION";
    }
}
