namespace Friendly_.Models
{
    // Modelo para representar uma fatura
    public class Invoice
    {
        // Identificador único da fatura
        public Guid id { get; set; }

        // Nome do cliente associado à fatura
        public string nomeCliente { get; set; }

        // Endereço de e-mail do cliente associado à fatura
        public string emailCliente { get; set; }

        // Data do dia da reserva
        public string dataReserva {  get; set; }

        // Hora do jogo relacionada à fatura
        public string horaJogo { get; set; }

        // Preço associado à fatura
        public decimal preco { get; set; }
    }
}