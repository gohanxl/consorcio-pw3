using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ConsorcioRepository : BaseRepository<Consorcio>
    {
        public ConsorcioRepository(ConsortiumContext context) : base(context)
        {
        }

        override public List<Consorcio> GetAll()
        {
            return ctx.Consorcio.OrderBy(c => c.Nombre).ToList();
        }
    }
}
