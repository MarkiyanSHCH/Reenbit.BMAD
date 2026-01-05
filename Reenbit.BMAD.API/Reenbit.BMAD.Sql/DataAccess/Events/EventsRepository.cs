using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Reenbit.BMAD.Core.Entities.Events;
using Reenbit.BMAD.Domain.Entities;
using Reenbit.BMAD.Domain.Result;
using Reenbit.BMAD.Sql.Common;
using Reenbit.BMAD.Sql.DataAccess.Events.Models;
using System.Data;
using static Dapper.SqlMapper;

namespace Reenbit.BMAD.Sql.DataAccess.Events
{
    public class EventsRepository : IEventsGateway
    {
        private readonly IDbConnectionHandler _dbConnectionHandler;
        private readonly ILogger<EventsRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionHandler">The database connection handler for creating database connections.</param>
        /// <param name="logger">The logger instance for logging operations and errors.</param>
        public EventsRepository(IDbConnectionHandler dbConnectionHandler,
            ILogger<EventsRepository> logger)
        {
            _dbConnectionHandler = dbConnectionHandler;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves events.
        /// </summary>
        /// <returns>
        /// An <see cref="IResult{T}"/> containing a collection of <see cref="Event"/> objects,
        /// or an error result if retrieval fails.
        /// </returns>
        public async Task<IResult<IEnumerable<Event>>> GetAsync()
        {
            try
            {
                using var connection = _dbConnectionHandler.CreateConnection();
                GridReader results = await connection.QueryMultipleAsync(
                    sql: "dbo.spEvents_GetEvents",
                    commandType: CommandType.StoredProcedure);

                IEnumerable<EventDto>? eventsDto = await results.ReadAsync<EventDto>();

                return Result.Success(eventsDto.Select(dto => dto.ToModel()));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to get events");

                return Result.Failed<IEnumerable<Event>>("Failed to get events", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");

                return Result.Failed<IEnumerable<Event>>("An unexpected error occurred", ex);
            }
        }

        /// <summary>
        /// Create the Event for a given user.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <param name="type">The type.</param>
        /// <param name="description">The description.</param>
        /// <returns>
        /// An <see cref="IResult"/> indicating the success or failure of the update operation.
        /// </returns>
        public async Task<IResult> CreateAsync(string userId, EventType type, string description)
        {
            try
            {
                DynamicParameters parameters = new();
                parameters.Add("@UserId", userId, DbType.String, ParameterDirection.Input);
                parameters.Add("@TypeId", type, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Description", description, DbType.String, ParameterDirection.Input);

                using var connection = _dbConnectionHandler.CreateConnection();
                await connection.ExecuteAsync(
                    sql: "dbo.spEvents_InsertEvent",
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                );

                return Result.Success();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to create event");

                return Result.Failed("Failed to create event", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");

                return Result.Failed("An unexpected error occurred", ex);
            }
        }
    }
}
