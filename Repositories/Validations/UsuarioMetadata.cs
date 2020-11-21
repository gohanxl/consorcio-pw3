using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UsuarioMetadata
    {
        [Required(ErrorMessage = "El campo de Email es obligatorio")]
        [EmailAddress(ErrorMessage = "No es un formato de Email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo de Contraseña es obligatorio")]
        public string Password { get; set; }
    }
}
