using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                
        public string Descripcion { get; set; }

        [DisplayName("Consorcio")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public Int32 IdConsorcio { get; set; }

        [DisplayName("Tipo de gasto")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public Int32 IdTipoGasto { get; set; }

        [Required(ErrorMessage = "La fecha de gasto es obligatorio")]
        public DateTime FechaGasto { get; set; }

        [DisplayName("Año")]
        [Required(ErrorMessage = "El año de expensa es obligatorio")]
        public Int32 AnioExpensa { get; set; }

        [DisplayName("Mes")]
        [Required(ErrorMessage = "El mes de expensa es obligatorio")]
        public Int32 MesExpensa { get; set; }

        [DisplayName("Comprobante")]
        [Required(ErrorMessage = "El archivo del comprobante es obligatorio")]
        public string ArchivoComprobante { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public decimal Monto { get; set; }

        [DisplayName("Fecha de creación")]
        [Required(ErrorMessage = "La fecha de creacion es obligatorio")]
        public DateTime FechaCreacion { get; set; }
    
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public Int32 IdUsuarioCreador { get; set; }
    }
}
