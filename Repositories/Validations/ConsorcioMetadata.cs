using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    class ConsorcioMetadata
    {
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Nombre { get; set; }

        [DisplayName("Provincia")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public int IdProvincia { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public int Altura { get; set; }

        [DisplayName("Día de vencimiento de expensas")]
        [Range(1, 28, ErrorMessage = "El día debe ser entre el {1} y el {2}")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public int DiaVencimientoExpensas { get; set; }
    }
}
