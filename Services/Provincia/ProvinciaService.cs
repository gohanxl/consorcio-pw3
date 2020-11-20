using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProvinciaService : BaseService<ProvinciaRepository, Provincia>
    {
        public ProvinciaService(ConsortiumContext context) : base(context)
        {
        }
    }
}
