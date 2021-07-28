using CCMERP.Domain.Settings;
using System.Threading.Tasks;

namespace CCMERP.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
