
using System.ComponentModel;

namespace MediatorExample.Domain.Entities.Enums
{
    public enum CommandType
    {
        [Description("Addition")]
        Add,
        [Description("Update")]
        Update,
        [Description("Delete")]
        Delete
    }
}
