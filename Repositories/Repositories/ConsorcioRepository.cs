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

        public override void Update(Consorcio obj)
        {
            Consorcio consorcioFound = GetById(obj.IdConsorcio);
            consorcioFound.Nombre = obj.Nombre;
            consorcioFound.IdProvincia = obj.IdProvincia;
            consorcioFound.Ciudad = obj.Ciudad;
            consorcioFound.Calle = obj.Calle;
            consorcioFound.Altura = obj.Altura;
            consorcioFound.DiaVencimientoExpensas = obj.DiaVencimientoExpensas;
            consorcioFound.Provincia = obj.Provincia;
            ctx.SaveChanges();
        }
    }
}
