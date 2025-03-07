using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Friendly_.Models
{
    public class RecintoDesportivo
    {
        [Key]
        public int recintoID { get; set; }
        
        [Required(ErrorMessage = "Especifique um Nome para o recinto")]
        [MaxLength(100)]
        [DisplayName("Nome Recinto")]
        public String name { get; set;}

        [Required(ErrorMessage = "Especifique um Concelho")]
        [MaxLength(100)]
        [DisplayName("Concelho")]
        public String concelho { get; set; }

        [Required]
        [DisplayName("Latitude")]
        public String latitude {  get; set; }
        
        [Required]
        [DisplayName("Longitude")]
        public String longitude { get; set; }
        
        [Required(ErrorMessage = "Especifique a modalidade")]
        [RegularExpression(@"^[a-zA-Z ]*$",ErrorMessage = "A Modalidade não é válida")]
        [DisplayName("Modalidade")]
        public String modalidade { get; set; }
        
        [Required]
        [DisplayName("Preço p/hora")]
        public float preco { get; set; }

        [RegularExpression(@"(9[1236][0-9]) ?([0-9]{3}) ?([0-9]{3})", ErrorMessage = "Nº de telemóvel inválido.")]
        [DisplayName("Contacto")]
        public String? contacto { get; set; }

        [Required]
        public String imagem { get; set; }


        [DisplayName("Descrição Recinto")]
        public String description { get; set; }
        
        public ICollection<Reserva>? Reservas { get; set; }

    }
}
