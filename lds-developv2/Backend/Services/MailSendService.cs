using MailKit.Security;
using MimeKit;
using Friendly_.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Friendly_.Interfaces;
using Microsoft.Extensions.Options;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace Friendly_.Services
{
    // Serviço responsável pelo envio de e-mails
    public class MailSendService : IMailSendService
    {
        // Configurações de e-mail injetadas via IOptions
        private readonly MailSettings _mailSettings;

        // Construtor que recebe as configurações de e-mail como dependência
        public MailSendService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        // Método para enviar e-mail de forma assíncrona
        public async Task SendMailAsync(String toEmail, String Subject, String AlternativeView, String pdfFilePath)
        {
            // Cria uma nova mensagem para o e-mail
            var email = new MimeMessage();

            // Configura o remetente, o destinatário e o assunto do email
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = $"{Subject} ({Guid.NewGuid().ToString("N").Substring(0, 8)})";

            // Constrói o corpo da mensagem
            var builder = new BodyBuilder();
            builder.HtmlBody = AlternativeView;

            // Anexa um arquivo PDF, se este for fornecido
            if (!string.IsNullOrEmpty(pdfFilePath))
            {
                var pdfAttachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(File.OpenRead(pdfFilePath), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,

                    FileName = Path.GetFileName(pdfFilePath)
                };

                builder.Attachments.Add(pdfAttachment);
            }

            // Configura o corpo da mensagem.
            email.Body = builder.ToMessageBody();

            // Utiliza cliente SMTP para enviar o e-mail
            using (var smpt = new SmtpClient())
            {
                smpt.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smpt.Authenticate(_mailSettings.Mail, _mailSettings.Password);

                // Envia o e-mail
                await smpt.SendAsync(email);

                // Desconecta do servidor SMTP
                smpt.Disconnect(true);
            }
        }

        // Método para criar um ficheiro PDF.
        public Task CreateInvoicePdf(Invoice invoice)
        {
            // Cria um novo documento PDF
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            // Define o tipo e tamanho de letra para o documento PDF
            XFont fontRegular = new XFont("Helvetica", 14);
            XFont fontBold = new XFont("Helvetica-Bold", 24);

            // Adiciona elementos ao documento PDF
            gfx.DrawString("Fatura", fontBold, XBrushes.Black, new XRect(0, 0, page.Width, 50), XStringFormats.Center);

            float lineHeight = 20; // Espaço entre linhas

            gfx.DrawString($"Nome do cliente: " + invoice.nomeCliente, fontRegular, XBrushes.Black, 50, 50 + lineHeight);
            gfx.DrawString($"Email do cliente: " + invoice.emailCliente, fontRegular, XBrushes.Black, 50, 50 + 2 * lineHeight);
            gfx.DrawString($"Preço da reserva: " + invoice.preco, fontRegular, XBrushes.Black, 50, 70 + 3 * lineHeight);
            gfx.DrawString($"Data da reserva: " + invoice.dataReserva, fontRegular, XBrushes.Black, 50, 70 + 4 * lineHeight);
            gfx.DrawString($"Hora do jogo: " + invoice.horaJogo, fontRegular, XBrushes.Black, 50, 70 + 5 * lineHeight);

            // Salva o arquivo PDF na pasta Invoices
            string filePath = @"Views/Invoices/" + invoice.id + ".pdf";
            document.Save(filePath);

            // Retorna o caminho do arquivo PDF criado
            return Task.FromResult(filePath);
        }
    }
}
