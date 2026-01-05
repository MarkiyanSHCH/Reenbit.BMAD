using Reenbit.BMAD.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Reenbit.BMAD.API.Controllers.Events.Models
{
    public record PostEventRequest(
        [Required] string UserId,
        [Required] EventType Type,
        [Required] string Description
    );
}
