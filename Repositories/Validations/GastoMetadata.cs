using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GastoMetadata
    {
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La {0} es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public Int32 IdConsorcio { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public Int32 IdTipoGasto { get; set; }

        [Required(ErrorMessage = "La fecha de gasto es obligatorio")]
        public DateTime FechaGasto { get; set; }

        [Required(ErrorMessage = "El año de expensa es obligatorio")]
        public Int32 AnioExpensa { get; set; }

        [Required(ErrorMessage = "El mes de expensa es obligatorio")]
        public Int32 MesExpensa { get; set; }

        [Required(ErrorMessage = "El archivo del comprobante es obligatorio")]
        public string ArchivoComprobante { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha de creacion es obligatorio")]
        public DateTime FechaCreacion { get; set; }
    
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public Int32 IdUsuarioCreador { get; set; }
    }
}
