using Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Gasto
{
    public class GastoEntity
    {
        [Required(ErrorMessage = "El campo de Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo de Descripcion es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo de IdConsorcio es obligatorio")]
        public Int32 IdConsorcio { get; set; }

        [Required(ErrorMessage = "El campo de IdTipoGasto es obligatorio")]
        public Int32 IdTipoGasto { get; set; }

        [Required(ErrorMessage = "El campo de FechaGasto es obligatorio")]
        public DateTime FechaGasto { get; set; }

        [Required(ErrorMessage = "El campo de AnioExpensa es obligatorio")]
        public Int32 AnioExpensa { get; set; }

        [Required(ErrorMessage = "El campo de MesExpensa es obligatorio")]
        public Int32 MesExpensa { get; set; }

        [Required(ErrorMessage = "El campo de ArchivoComprobante es obligatorio")]
        public string ArchivoComprobante { get; set; }

        [Required(ErrorMessage = "El campo de Monto es obligatorio")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El campo de FechaCreacion es obligatorio")]
        public DateTime FechaCreacion { get; set; }
    
        [Required(ErrorMessage = "El campo de IdUsuarioCreador es obligatorio")]
        public Int32 IdUsuarioCreador { get; set; }
    }
}
