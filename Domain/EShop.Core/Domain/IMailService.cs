using EShop.Core.Entities;
using System.Threading.Tasks;

namespace EShop.Core.Domain
{
    public interface IMailService
    {
        Task<bool> SendOrderCreatedMailAsync(Order order);
        Task<bool> SendOrderStatusChangedMailAsync(Order order);
    }
}