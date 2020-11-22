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

        public List<Unidad> GetAllByConsorcioId(int consorcioId)
        {
            return repo.GetAllByConsorcioId(consorcioId);
        }

        public int CountUnidadesByConsorcioId(int consorcioId)
        {
            return repo.CountUnidadesByConsorcioId(consorcioId);
        }
    }
}
