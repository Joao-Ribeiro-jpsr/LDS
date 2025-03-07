using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Friendly_.Models
{
    public class Reserva
    {
        [Key]
        public int reservaID { get; set; }
        
        public RecintoDesportivo? recinto {  get; set; }
        public int recintoID { get; set; }
        
        public User? user { get; set; }
        public int userID { get; set; }
        
 
        public UserContacts? userContact { get; set; }
        public int? userContactID { get; set; }

        public Pagamento? pagamento { get; set; }
        public int? pagamentoID { get; set; }

        [Required]
        [DisplayName("Data")]
        public string dataInicial { get; set; }

        [Required]
        [DisplayName("Hora Reserva")]
        public string horaReserva { get; set; }

        [Required]
        [DisplayName("Hora Jogo")]
        public string horaJogo{ get; set; }


        [DisplayName("Hora de Cancelamento")]
        public string? horaCancelamento { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")] // Adjust size based on your needs
        [DisplayName("Preço")]
        public decimal preco { get; set; }

        [Required]
        [DisplayName("Estado da Reserva")]
        [MaxLength(20)]
  
        public String estado { get;set; }
    }
}
