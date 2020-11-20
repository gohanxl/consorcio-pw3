using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Repositories
{
    public class TipoGastoRepository : BaseRepository<TipoGasto>
    {
        public TipoGastoRepository(ConsortiumContext context) : base(context)
        { 
        }
    }
}
