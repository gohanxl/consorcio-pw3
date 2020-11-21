using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected ConsortiumContext ctx;
        DbSet<T> dbSet;

        public BaseRepository(ConsortiumContext context)
        {
            ctx = context;
            dbSet = ctx.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = dbSet.Find(id);
            dbSet.Remove(existing);
            Save();
        }

        public virtual List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T obj)
        {
            dbSet.Add(obj);
            Save();
        }

        public void Save()
        {
            try
            {
                ctx.SaveChanges();
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
            dbSet.Attach(obj);
            ctx.Entry(obj).State = EntityState.Modified;
            Save();
        }
    }
}
