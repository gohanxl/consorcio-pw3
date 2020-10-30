using Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

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
            if(obj is Usuario user)
            {
                user.Password = HashPassword(user.Password);
            }
            defaultObject.Add(obj);
            Save();
        }

        public void Save()
        {   
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
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

        public Usuario IsUserValid(string email, string password)
        {
            Usuario userFound = context.Usuario.Single(user => user.Email == email);
            if (userFound == null)
            {
                return null;
            }
            if (!VerifyPassword(password, userFound.Password))
            {
                return null;
            }
            return userFound;
        }

        private string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }
    }
}
