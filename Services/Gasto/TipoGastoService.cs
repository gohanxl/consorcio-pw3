using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Repositories;

namespace Services.Gasto
{
    public class TipoGastoService<T> where T : class
    {
        TipoGastoRepository<T> tipoGastoRepository;

        public TipoGastoService(ConsortiumContext context)
        {
            ConsortiumContext ctx = context;
            tipoGastoRepository = new TipoGastoRepository<T>(ctx);
        }

        public void Delete(object id)
        {
            tipoGastoRepository.Delete(id);
        }

        public T GetById(object id)
        {
            return tipoGastoRepository.GetById(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            tipoGastoRepository.Insert(obj);
        }

        public void Save()
        {
            tipoGastoRepository.Save();
        }

        public void Update(T obj)
        {
            tipoGastoRepository.Update(obj);
        }
        public IEnumerable<T> GetAll()
        {
            return tipoGastoRepository.GetAll();
        }
    }
}
