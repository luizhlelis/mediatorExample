
using System.ComponentModel;

namespace MediatorExample.Domain.Entities.Enums
{
    public enum EventType
    {
        [Description("Query")]
        Query,
        [Description("Command")]
        Command
    }
}
