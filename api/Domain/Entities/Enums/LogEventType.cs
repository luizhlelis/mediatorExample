
using System.ComponentModel;

namespace MediatorExample.Domain.Entities.Enums
{
    public enum EventAction
    {
        [Description("Addition")]
        Add,
        [Description("Update")]
        Update,
        [Description("Remove")]
        Delete,
        [Description("Get One")]
        Get,
        [Description("Get All")]
        GetAll
    }
}
