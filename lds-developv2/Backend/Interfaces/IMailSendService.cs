using Friendly_.Models;
using System.Net.Mail;
using System.Reflection;

namespace Friendly_.Interfaces
{
    public interface IMailSendService
    {
        Task SendMailAsync(String toEmail, String Subject, String AlternativeView, String pdfFilePath);
        Task CreateInvoicePdf(Invoice invoice);
    }
}
