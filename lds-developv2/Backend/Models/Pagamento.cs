using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Friendly_.Models
{
    public class Pagamento
    {
        [Key]
        public int pagamentoID { get; set; }
        
        public User? User { get; set; }
        public int userID {  get; set; }

        public MetodoPagamento? metodo { get; set; }
        public int metodoID { get; set;}
        
        [Required]
        [DisplayName("Total a pagar")]
        public double total {  get; set; }
    }
}
