using System.ComponentModel;

namespace EShop.Core.Entities.Enums
{
    public enum OrderStatus
    {
        [Description("New")]
        New,
        [Description("In progress")]
        InProgress,
        [Description("Completed")]
        Completed,
        [Description("Cancelled")]
        Cancelled,
    }
}