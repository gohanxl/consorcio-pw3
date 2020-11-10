using Entities;
using Repositories;
using Repositories.Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class UsuarioService<T> where T : class
    {
        UsuarioRepository<T> userRepository = new UsuarioRepository<T>();

        public void Delete(object id)
        {
            userRepository.Delete(id);
        }

        public T GetById(object id)
        {
            return userRepository.GetById(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            userRepository.Insert(obj);
        }

        public void Save()
        {
            userRepository.Save();
        }

        public void Update(T obj)
        {
            userRepository.Update(obj);
        }
        public IEnumerable<T> GetAll()
        {
            return userRepository.GetAll();
        }

        public bool EmailExist(string email)
        {
            return userRepository.EmailExist(email);
        }

        public Usuario GetByEmail(string email)
        {
            return userRepository.GetByEmail(email);
        }

        public Usuario IsUserValid(string email, string password)
        {
            return userRepository.IsUserValid(email, password);
        }
    }
}
