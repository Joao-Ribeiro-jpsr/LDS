using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Friendly_.Models
{
    public class MetodoPagamento
    {
        [Key]
        public int metodoID { get; set; }
        
        [Required]
        [DisplayName("Método de Pagamento")]
        public String tipo { get; set; }
        
        public String descricao { get; set; }

        public ICollection<Pagamento>? Pagamentos { get; set; }
    }
}
