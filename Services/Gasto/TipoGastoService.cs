using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Services
{
    public class TipoGastoService : BaseService<TipoGastoRepository, TipoGasto>
    {
        public TipoGastoService(ConsortiumContext context) : base(context)
        {
        }
    }
}
