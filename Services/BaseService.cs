using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaseService<T, J> : IService<J> where T : BaseRepository<J> where J : class
    {
        protected T repo;
        
        public BaseService(ConsortiumContext context)
        {
            ConsortiumContext ctx = context;
            repo = Activator.CreateInstance(typeof(T), new object[] { context }) as T;
        }

        public void Delete(object id)
        {
            repo.Delete(id);
        }

        public List<J> GetAll()
        {
            return repo.GetAll();
        }

        public J GetById(object id)
        {
            return repo.GetById(id);
        }

        public void Insert(J obj)
        {
            repo.Insert(obj);
        }

        public void Update(J obj)
        {
            repo.Update(obj);
        }
    }
}
