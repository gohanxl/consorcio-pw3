using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Repositories
{
    public class GastoRepository : BaseRepository<Gasto>
    {
        public GastoRepository(ConsortiumContext context) : base(context)
        { 
        }
    }
}
