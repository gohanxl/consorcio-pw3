using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GastoService : BaseService<GastoRepository, Gasto>
    {
        public GastoService(ConsortiumContext context) : base(context)
        {
        }

        public decimal GetGastosTotalCurrentMonthByConsorcio(int consorcioId)
        {
            return repo.GetGastosTotalCurrentMonthByConsorcio(consorcioId);
        }

        public List<ExpensaDTO> GetExpensasById(int consorcioId)
        {
            return repo.GetExpensasById(consorcioId);
        }
    }
}
