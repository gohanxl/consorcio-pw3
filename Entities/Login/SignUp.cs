using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Login
{
    public class SignUp
    {
        [Required(ErrorMessage = "El campo de Email es obligatorio")]
        [EmailAddress(ErrorMessage = "No es un formato de Email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo de Contraseña es obligatorio")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,64}$", ErrorMessage = "Contraseña debil")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Las contraseñas no son identicas, intente de nuevo.")]
        public string ConfirmPassword { get; set; }
    }
}
