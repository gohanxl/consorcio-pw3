using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ExpensaDTO
    {
        public int AnioExpensa { get; set; }
        public int MesExpensa { get; set; }
        public decimal GastoTotal { get; set; }
        public decimal ExpensasPorUnidad { get; set; }
    }
}
