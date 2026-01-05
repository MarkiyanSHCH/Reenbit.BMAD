namespace Reenbit.BMAD.Domain.Entities
{
    /// <summary>
    /// Changes to enum should be synchronized with DB and vice versa.
    /// </summary>
    public enum EventType
    {
        PageView = 1,
        Click,
        Purchase
    }
}
