using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Friendly_.Models
{
    public class User
    {
        [Key]
        public int userID { get; set; }

        public UserContacts? userContact { get; set; }
        public int? userContactID { get; set; }

        [Required(ErrorMessage = "Especifique um Nome")]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public String nome { get; set; }

        [Required(ErrorMessage = "Especifique um Email válido")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "O email não é válido")]
        [DisplayName("Email")]
        public String email { get; set; }

        [Required(ErrorMessage = "Especifique uma Password")]
        [DisplayName("Password")]
        public String password { get; set; }

        public Boolean isAdmin { get; set; }

        [MaxLength(100)]
        [DisplayName("Morada")]
        public String morada { get; set; }

        [DisplayName("Data de Nascimento")]
        public string nascimento { get; set; }


        [DisplayName("Género")]
        [MaxLength(1)]
        [MinLength(1)]
        [RegularExpression(@"(?:m|M|f|F|i|I)$", ErrorMessage = "Apenas são permitidos os valores m/M - Masculino, f/F - Feminino, i/I - Indeterminado")]
        public String genero { get; set; }

        [RegularExpression(@"(9[1236][0-9]) ?([0-9]{3}) ?([0-9]{3})", ErrorMessage = "Nº de telemóvel inválido.")]
        [DisplayName("Telemóvel")]
        public String telemovel { get; set; }

        [DisplayName("NIF")]
        [MinLength(9)]
        [MaxLength(9)]
        public String nif { get; set; }

        public int pontos { get; set; }

        public Boolean isAccountActivated { get; set; }
    }
}