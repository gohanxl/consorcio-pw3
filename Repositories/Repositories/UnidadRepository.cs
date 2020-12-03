using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Repositories
{
    public class UnidadRepository : BaseRepository<Unidad>
    {
        public UnidadRepository(ConsortiumContext context) : base(context)
        {
        }

        public List<Unidad> GetAllByConsorcioId(int consorcioId)
        {
            List<Unidad> unidades = ctx.Unidad
                .Where(u => u.IdConsorcio == consorcioId)
                .OrderBy(u => u.Nombre)
                .ToList();
            return unidades;
        }

        public int CountUnidadesByConsorcioId(int consorcioId)
        {
            int count = ctx.Unidad
                .Where(u => u.IdConsorcio == consorcioId)
                .Count();
            return count;
        }

        public bool ValidateCreatorWithCurrentUser(string currentUserEmail, string creatorUserEmail)
        {
            return currentUserEmail.Equals(creatorUserEmail);
        }
    }
}
