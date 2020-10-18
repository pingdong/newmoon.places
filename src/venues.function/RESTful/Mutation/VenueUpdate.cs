using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PingDong.Azure.Function;
using PingDong.Http;
using PingDong.Newmoon.Venues.Services.Commands;
using PingDong.Newmoon.Venues.Settings;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;

namespace PingDong.Newmoon.Venues.Endpoints
{
    public class VenueUpdate : HttpCommandTrigger
    {
        private readonly IOptionsMonitor<AppSettings> _settings;

        public VenueUpdate(
            TelemetryConfiguration telemetryConfiguration
            , IMediator mediator
            , IHttpContextAccessor accessor
            , IHttpRequestHelper requestHelper
            , ILogger<VenueClose> logger
            , IValidatorFactory validatorFactory
            , IOptionsMonitor<AppSettings> settings
        ) : base(telemetryConfiguration, accessor, requestHelper
            , mediator, logger, validatorFactory)
        {
            _settings = settings;
        }

        [FunctionName("Venue_Update")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "venue/{venueid}/update")] HttpRequest request
            , ExecutionContext context
            , string venueId)
        {
            return await ProcessAsync<VenueUpdateCommand>(context, request, _settings.CurrentValue.SupportIdempotencyCheck).ConfigureAwait(false);
        }
    }
}
