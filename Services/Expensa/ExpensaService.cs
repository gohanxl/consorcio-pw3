using Repositories;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Expensa
{
    public class ExpensaService<T> where T : class
    {
        ExpensaRepository<T> expensaRepository;

        public ExpensaService(ConsortiumContext context)
        {
            ConsortiumContext ctx = context;
            expensaRepository = new ExpensaRepository<T>(ctx);
        }

        public void Delete(object id)
        {
            expensaRepository.Delete(id);
        }

        public T GetById(object id)
        {
            return expensaRepository.GetById(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            expensaRepository.Insert(obj);
        }

        public void Save()
        {
            expensaRepository.Save();
        }

        public void Update(T obj)
        {
            expensaRepository.Update(obj);
        }
        public IEnumerable<T> GetAll()
        {
            return expensaRepository.GetAll();
        }
    }
}
