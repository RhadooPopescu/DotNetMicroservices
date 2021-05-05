using Ordering.Application.Models;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Infrastructure
{
    //The email service interface will be implemented under the Ordering.Infrstructure layer.
    //Email object and settings are implemented under the models folder.
    public interface IEmailService
    {
        //This method sends an email to the customer when an order is complete.
        Task<bool> SendEmail(Email email);
    }
}
