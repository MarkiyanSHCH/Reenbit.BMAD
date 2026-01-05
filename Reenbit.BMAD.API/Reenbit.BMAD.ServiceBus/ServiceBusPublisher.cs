using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Reenbit.BMAD.Core.Internal.ServiceBus;
using Reenbit.BMAD.Core.Internal.ServiceBus.Models;
using Reenbit.BMAD.Domain.Result;
using System.Text.Json;

namespace Reenbit.BMAD.ServiceBus
{
    public class ServiceBusPublisher : IServiceBusPublisher
    {
        private const string QueueName = "events";
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger<ServiceBusPublisher> _logger;

        public ServiceBusPublisher(ServiceBusClient serviceBusClient, ILogger<ServiceBusPublisher> logger)
            => (_serviceBusClient, _logger) = (serviceBusClient, logger);

        public async Task<IResult> SendMessageAsync(EventCreatedEvent @event)
        {
            try
            {
                string messageBody = JsonSerializer.Serialize(@event);
                ServiceBusSender sender = _serviceBusClient.CreateSender(QueueName);
                ServiceBusMessage message = new(messageBody);

                await sender.SendMessageAsync(message);
                _logger.LogInformation($"Sent message to {QueueName}");

                await sender.DisposeAsync();

                return Result.Success();
            }
            catch (ServiceBusException ex)
            {
                _logger.LogError(ex, "Failed to send message");

                return Result.Failed("Failed to send message", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");

                return Result.Failed("Failed to send message", ex);
            }
        }
    }
}
