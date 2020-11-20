using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Repositories
{
    public class ProvinciaRepository : BaseRepository<Provincia>
    {
        public ProvinciaRepository(ConsortiumContext context) : base(context)
        {
        }
    }
}
