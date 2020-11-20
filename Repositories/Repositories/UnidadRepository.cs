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
    }
}
