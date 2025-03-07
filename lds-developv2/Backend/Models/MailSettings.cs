namespace Friendly_.Models
{
    // Modelo para armazenar configurações de e-mail
    public class MailSettings
    {
        // Endereço de e-mail a ser utilizado para enviar e-mails
        public string Mail { get; set; }

        // Nome associado ao endereço de e-mail
        public string DisplayName { get; set; }

        // Senha associada ao endereço de e-mail
        public string Password { get; set; }

        // Host do servidor SMTP utilizado para envio de e-mails
        public string Host { get; set; }

        // Porta do servidor SMTP utilizada para envio de e-mails
        public int Port { get; set; }
    }
}