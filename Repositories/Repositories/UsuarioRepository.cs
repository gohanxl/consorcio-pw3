using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Helpers;

namespace Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>
    {
        public UsuarioRepository(ConsortiumContext context) : base(context)
        {
        }

        override public void Insert(Usuario user)
        {
            user.Password = HashPassword(user.Password);
            ctx.Usuario.Add(user);
            Save();
        }

        public bool EmailExist(string email)
        {
            var hasUserWithEmail = ctx.Usuario.Any(user => user.Email == email);

            return hasUserWithEmail;
        }

        public Usuario GetByEmail(string email)
        {
            return ctx.Usuario.FirstOrDefault(user => user.Email == email);
        }

        public Usuario IsUserValid(string email, string password)
        {
            Usuario userFound = ctx.Usuario.Single(user => user.Email == email);
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
