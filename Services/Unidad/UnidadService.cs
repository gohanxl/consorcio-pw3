using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UnidadService : BaseService<UnidadRepository, Unidad>
    {
        public UnidadService(ConsortiumContext context) : base(context)
        {
        }
    }
}
