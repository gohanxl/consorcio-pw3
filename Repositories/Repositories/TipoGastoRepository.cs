using Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Repositories.Repositories
{
    public class TipoGastoRepository<T> : IEnumerable<T>, IRepository<T> where T : class
    {
        ConsortiumContext tipoGastoContext;
        private DbSet<T> defaultObject;

        public TipoGastoRepository(ConsortiumContext context)
        {
            tipoGastoContext = context;
            defaultObject = tipoGastoContext.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = defaultObject.Find(id);
            defaultObject.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return defaultObject.ToList();
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
            try
            {
                tipoGastoContext.SaveChanges();
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
            tipoGastoContext.Entry(obj).State = EntityState.Modified;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
