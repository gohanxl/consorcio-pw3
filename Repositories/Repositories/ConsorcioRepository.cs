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

        public List<Consorcio> GetAllByUser(int userId)
        {
            return ctx.Consorcio
                .Where(c => c.IdUsuarioCreador == userId)
                .OrderBy(c => c.Nombre).ToList();
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

        public bool ValidateCreatorWithCurrentUser(string currentUserEmail, string creatorUserEmail)
        {
            return currentUserEmail.Equals(creatorUserEmail);
        }
    }
}
