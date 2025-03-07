using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Friendly_.Models
{
    public class UserContacts
    {
        [Key]
        public int? userContactID { get; set; }
        
        [Required]
        [DisplayName("Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "O email não é válido")]
        public String email { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
