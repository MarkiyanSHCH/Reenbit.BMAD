using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Reenbit.BMAD.Core.Entities.Events;
using Reenbit.BMAD.Core.Internal.ServiceBus.Models;
using System.Text.Json;

using IResult = Reenbit.BMAD.Domain.Result.IResult;

namespace Reenbit.BMAD.FunctionApp
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly IEventsGateway _eventsGateway;

        public Function1(ILogger<Function1> logger, IEventsGateway eventsGateway)
            => (_logger, _eventsGateway) = (logger, eventsGateway);

        [Function(nameof(Function1))]
        public async Task Run(
            [ServiceBusTrigger("events", Connection = "azure-service-bus")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            EventCreatedEvent? request = JsonSerializer.Deserialize<EventCreatedEvent>(message.Body);

            if (request == null)
            {
                _logger.LogError("Message empty");

                return;
            }

            IResult resultInsert = await _eventsGateway.CreateAsync(request.UserId, request.Type, request.Description);

            if (resultInsert.IsFailed)
            {
                _logger.LogError(resultInsert.Error.Exception, "Failed to insert event");

                return;
            }

            await messageActions.CompleteMessageAsync(message);
        }
    }
}
