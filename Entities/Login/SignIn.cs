using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Login
{
    public class SignIn
    {
        [Required(ErrorMessage = "El campo de Email es obligatorio")]
        [EmailAddress(ErrorMessage = "No es un formato de Email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo de Contraseña es obligatorio")]
        public string Password { get; set; }
    }
}
