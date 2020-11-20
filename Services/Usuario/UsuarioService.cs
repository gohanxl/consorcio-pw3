using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class UsuarioService : BaseService<UsuarioRepository, Usuario>
    {
        public UsuarioService(ConsortiumContext context) : base(context)
        {
        }

        public bool EmailExist(string email)
        {
            return repo.EmailExist(email);
        }

        public Usuario GetByEmail(string email)
        {
            return repo.GetByEmail(email);
        }

        public Usuario IsUserValid(string email, string password)
        {
            return repo.IsUserValid(email, password);
        }
    }
}
