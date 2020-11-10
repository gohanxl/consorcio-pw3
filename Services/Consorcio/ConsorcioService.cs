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
        ConsorcioRepository<T> gastoRepository = new ConsorcioRepository<T>();

        public void Delete(object id)
        {
            gastoRepository.Delete(id);
        }

        public T GetById(object id)
        {
            return gastoRepository.GetById(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            gastoRepository.Insert(obj);
        }

        public void Save()
        {
            gastoRepository.Save();
        }

        public void Update(T obj)
        {
            gastoRepository.Update(obj);
        }
        public IEnumerable<T> GetAll()
        {
            return gastoRepository.GetAll();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
