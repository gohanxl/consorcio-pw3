using Repositories;
using Repositories.Interfaces;
using Repositories.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Consorcio
{
    public class ConsorcioService<T> : IEnumerable<T>, IRepository<T> where T : class
    {

        ConsorcioRepository<T> consorcioRepository;

        public ConsorcioService(ConsortiumContext context)
        {
            ConsortiumContext ctx = context;
            consorcioRepository = new ConsorcioRepository<T>(ctx);
        }

        public void Delete(object id)
        {
            consorcioRepository.Delete(id);
        }

        public T GetById(object id)
        {
            return consorcioRepository.GetById(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            consorcioRepository.Insert(obj);
        }

        public void Save()
        {
            consorcioRepository.Save();
        }

        public void Update(T obj)
        {
            consorcioRepository.Update(obj);
        }
        public IEnumerable<T> GetAll()
        {
            return consorcioRepository.GetAll();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
