using Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UsuarioRepository<T> : IEnumerable<T>, IRepository<T> where T : class
    {
        ConsortiumContext context = null;
        private DbSet<T> defaultObject = null;

        public UsuarioRepository()
        {
            this.context = new ConsortiumContext();
            defaultObject = context.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = defaultObject.Find(id);
            defaultObject.Remove(existing);
        }

        public T GetById(object id)
        {
            return defaultObject.Find(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            defaultObject.Add(obj);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            defaultObject.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }
        public IEnumerable<T> GetAll()
        {
            return defaultObject.ToList();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool GetByEmail(string email)
        {
            var hasUserWithEmail = context.Usuario.Any(user => user.Email == email);

            return hasUserWithEmail;
        }

        public bool IsUserValid(string email, string password)
        {
            var isUserValid = context.Usuario.Any(user => user.Email == email && user.Password == password);

            return isUserValid;
        }
    }
}
