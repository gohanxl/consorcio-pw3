using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaRegistracion { get; set; }
        public DateTime UltimaFechaLogin { get; set; }
    }
}
