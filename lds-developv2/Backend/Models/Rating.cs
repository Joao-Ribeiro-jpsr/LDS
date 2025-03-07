using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Friendly_.Models
{
    public class Rating
    {
        [Key]
        public int ratingID { get; set; }

        public User? user { get; set; }
        [Required]
        public int userID { get; set; }

        public RecintoDesportivo? recinto { get; set; }
        [Required]
        public int recintoID { get; set; }

        [Required(ErrorMessage = "Especifique um Nome")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public String username { get; set; }

        public int rating { get; set; }


        [DisplayName("Descrição Recinto")]
        public String? description { get; set; }
    }
}
