using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo de Email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo de Contraseña es obligatorio")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,64}$", ErrorMessage = "Te la comes")]
        public string Password { get; set; }
        public DateTime FechaRegistracion { get; set; }
        public DateTime UltimaFechaLogin { get; set; }
    }
}
