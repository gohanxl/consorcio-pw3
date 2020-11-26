using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ExpensaDTO
    {
        [DisplayName("Año")]
        public int AnioExpensa { get; set; }

        [DisplayName("Mes")]
        public int MesExpensa { get; set; }

        [DisplayName("Gasto Total")]
        public decimal GastoTotal { get; set; }

        [DisplayName("Expensas por Unidad")]
        public decimal ExpensasPorUnidad { get; set; }
    }
}
