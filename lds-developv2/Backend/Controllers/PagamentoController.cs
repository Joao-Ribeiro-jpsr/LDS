using Friendly_.Data;
using Friendly_.Interfaces;
using Friendly_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Friendly_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private AgendadorEmail _email;
        private IMailSendService _mailSendService;
        public PagamentoController(ApplicationDbContext db, AgendadorEmail agendadorEmail, IMailSendService mailService)
        {
            _db = db;
            _email = agendadorEmail;
            _mailSendService = mailService;
        }

        /**
        * Este metodo permite efetuar os pagamentos
        */
        [HttpPost("createpagamento/{id}/{pontos}")]
        public async Task <ActionResult<Pagamento>> CreatePagamento(int id, int pontos, Pagamento newPagamento)
        {
            if (newPagamento == null)
            {
                return NotFound();
            }

            Reserva reserva = _db.Reserva.Find(id);


            bool tempoExpirado = ReservasController.VerificarTempoExpirado(reserva);

            if (tempoExpirado) { 
                return StatusCode(400, "Reserva expirada");
            }

            if (reserva == null) return NotFound();

            User user = _db.User.Find(reserva.userID);


            if (pontos != null) { user.pontos -= pontos; }

            user.pontos += 1;
            _db.User.Update(user);

            _db.Pagamento.Add(newPagamento);          
            _db.SaveChanges();


            reserva.preco = (decimal)newPagamento.total;
            reserva.pagamentoID = newPagamento.pagamentoID;
            reserva.estado = "Confirmada";
            _db.Reserva.Update(reserva);
            _db.SaveChanges();

            
            try
            {
                DateTime data = DateTime.Now;

                // Criando uma nova instância da classe Invoice
                var _invoice = new Invoice
                {
                    id = Guid.NewGuid(),
                    nomeCliente = user.nome,
                    emailCliente = user.email,
                    preco = reserva.preco,
                    dataReserva = data.ToString(),
                    horaJogo = reserva.dataInicial
                };

                // Cria um arquivo PDF da fatura e obtém o caminho do arquivo
                _mailSendService.CreateInvoicePdf(_invoice);

                // Caminho do arquivo PDF criado
                string pdfFilePath = "Views/Invoices/" + _invoice.id + ".pdf";

                // Caminho do template de e-mail
                String templatePath = "Views/EmailTemplates/ReserveConfirmationEmail.html";

                // Lê o conteúdo do template de e-mail
                string emailBody = System.IO.File.ReadAllText(templatePath);

                // Obtém informações do recinto a partir da base de dados
                RecintoDesportivo recinto = _db.Recintos.Find(reserva.recintoID);

                // Substitui placeholders no corpo do e-mail com informações necessárias
                emailBody = emailBody.Replace("[NOME]", user.nome);
                emailBody = emailBody.Replace("[SUBJECT]", "Confirmação de reserva");
                emailBody = emailBody.Replace("[DATAINICIAL]", reserva.dataInicial);
                emailBody = emailBody.Replace("[RECINTONOME]", recinto.name);

                // Envia o e-mail de confirmação com o PDF anexado
                await _mailSendService.SendMailAsync(user.email, "Confirmação de reserva", emailBody, pdfFilePath);

            }
            catch (Exception)
            {
                // Em caso de falha, retorna um código de status 500 com uma mensagem de erro
                return StatusCode(500, "O pagamento não pôde ser efetuado.");
            }

            _ = _email.AgendarEmailAsync(reserva);

            return Ok(newPagamento.pagamentoID);
            }
    }
}