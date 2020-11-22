using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    class UnidadMetadata
    {
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Nombre { get; set; }

        [DisplayName("Consorcio")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public int IdConsorcio { get; set; }

        [DisplayName("Nombre del propietario")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string NombrePropietario { get; set; }

        [DisplayName("Apellido del propietario")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string ApellidoPropietario { get; set; }

        [DisplayName("Email del propietario")]
        [EmailAddress(ErrorMessage = "No es un formato de Email válido")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public int EmailPropietario { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public int Superficie { get; set; }
    }
}
