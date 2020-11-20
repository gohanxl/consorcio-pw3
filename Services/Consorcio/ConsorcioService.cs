using Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ConsorcioService : BaseService<ConsorcioRepository, Consorcio>
    {
        public ConsorcioService(ConsortiumContext context) : base(context)
        { 
        }
    }
}
