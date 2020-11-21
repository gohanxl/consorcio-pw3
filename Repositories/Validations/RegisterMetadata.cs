using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RegisterMetadata
    {
        [Required(ErrorMessage = "El {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "No es un formato de Email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La {0} es obligatorio")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,64}$", ErrorMessage = "Contraseña debil")]
        public string Password { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "La {0} es obligatorio")]
        [Compare("Password", ErrorMessage = "Las contraseñas no son identicas")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
