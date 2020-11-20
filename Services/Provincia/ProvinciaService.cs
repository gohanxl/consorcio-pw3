using Repositories;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Provincia
{
    public class ProvinciaService<T> where T : class
    {
        ProvinciaRepository<T> provinciaRepository;

        public ProvinciaService(ConsortiumContext context)
        {
            ConsortiumContext ctx = context;
            provinciaRepository = new ProvinciaRepository<T>(ctx);
        }

        public void Delete(object id)
        {
            provinciaRepository.Delete(id);
        }

        public T GetById(object id)
        {
            return provinciaRepository.GetById(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            provinciaRepository.Insert(obj);
        }

        public void Save()
        {
            provinciaRepository.Save();
        }

        public void Update(T obj)
        {
            provinciaRepository.Update(obj);
        }
        public IEnumerable<T> GetAll()
        {
            return provinciaRepository.GetAll();
        }
    }
}
