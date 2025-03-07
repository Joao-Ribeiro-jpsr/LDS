using Friendly_.Data;
using Friendly_.Interfaces;
using Friendly_.Models;

public class AgendadorEmail : BackgroundService
{
    private readonly IMailSendService _emailService;

    // Construtor que recebe o serviço de envio de e-mail.
    public AgendadorEmail( IMailSendService emailService)
    {
        _emailService = emailService;
    }

    // Método para agendar o envio de e-mail com base na hora escolhida aquando da reserva.
    public async Task AgendarEmailAsync(Reserva reserva)
    {
        // Divide a hora da reserva em partes
        string[] partes = reserva.horaJogo.Split(' ');
        string horaInicial = partes[0].Trim();

        // Divide a hora inicial em horas e minutos.
        string[] partesHoraInicial = horaInicial.Split(':');
        int horas = Convert.ToInt32(partesHoraInicial[0]);
        int minutos = Convert.ToInt32(partesHoraInicial[1]);

        // Cria a data e hora do jogo com base na reserva.
        DateTime datEvento = DateTime.ParseExact(reserva.dataInicial, "yyyy-MM-dd", null);
        DateTime dataEvento = new DateTime(datEvento.Year, datEvento.Month, datEvento.Day, horas, minutos, 0);
        DateTime tempoParaEnviarEmail = dataEvento.AddMinutes(-2);

        if (tempoParaEnviarEmail > DateTime.Now)
        {
            TimeSpan atrasoInicial = tempoParaEnviarEmail - DateTime.Now;

            // Criando um timer para ativar o método EnviarEmail após o atraso inicial
            var timer = new Timer(state => EnviarEmail(reserva), null, atrasoInicial, Timeout.InfiniteTimeSpan);

            // Aguardar até que o timer seja ativado ou até que o tempo inicial expire
            await Task.Delay(atrasoInicial);
        }
    }

    // Método para enviar o e-mail.
    private async Task EnviarEmail(Reserva reserva)
    {
        try

        {
            // Caminho do arquivo do template de e-mail.
            String templatePath = "Views/EmailTemplates/ReserveNotification.html";
            string emailBody = File.ReadAllText(templatePath);
            //RecintoDesportivo recinto = _db.Recintos.Find(reserva.recintoID);

            // Substituir placeholders no corpo do e-mail com informações da reserva.
            emailBody = emailBody.Replace("[NOME]", reserva.user.nome);
            emailBody = emailBody.Replace("[SUBJECT]", "Notificação de reserva");
            emailBody = emailBody.Replace("[DATAINICIAL]", "teste");
            emailBody = emailBody.Replace("[RECINTONOME]", "Reecinto" );


            // Enviar e-mail de forma assíncrona usando o serviço de e-mail.
            await _emailService.SendMailAsync(reserva.user.email, "Notificação de reserva", emailBody, "");
        }
        catch (Exception)
        {
            return;
        }
    }

    // Método obrigatório devido à herança da classe BackgroundService.
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
