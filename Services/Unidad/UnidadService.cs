using Repositories;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Unidad
{
    public class UnidadService<T> where T : class
    {
        UnidadRepository<T> unidadRepository;

        public UnidadService(ConsortiumContext context)
        {
            ConsortiumContext ctx = context;
            unidadRepository = new UnidadRepository<T>(ctx);
        }

        public void Delete(object id)
        {
            unidadRepository.Delete(id);
        }

        public T GetById(object id)
        {
            return unidadRepository.GetById(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            unidadRepository.Insert(obj);
        }

        public void Save()
        {
            unidadRepository.Save();
        }

        public void Update(T obj)
        {
            unidadRepository.Update(obj);
        }
        public IEnumerable<T> GetAll()
        {
            return unidadRepository.GetAll();
        }
    }
}
