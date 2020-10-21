using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using NHibernate.Criterion;
using System.Security.Cryptography;

namespace Locadora.Services
{
    public partial class TUserService : EntityService<TUser>, ITUserService
    {
        public TUser FindByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            return Session.CreateCriteria<TUser>()
                .Add(Restrictions.Eq("Login", username))
                .SetMaxResults(1)
                .UniqueResult<TUser>();
        }

        public byte[] HashPassword(string password)
        {
            return SHA384.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public TUser Authenticate(Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
                return null;

            var user = Session.CreateCriteria<TUser>()
                .Add(Restrictions.Eq("Login", login.Username))
                .SetMaxResults(1)
                .UniqueResult<TUser>();

            if (user == null || user.Password == null)
                return null;

            var hashTryPassword = HashPassword(login.Password);
            return user.Password.SequenceEqual(hashTryPassword) ? user : null;
        }

        public void Edit(TUser model)
        {
            var client = TUser.Load(model.Id);
            client.Name = model.Name;
            client.EnumProfileUser = model.EnumProfileUser;
            client.Update();
        }
    }
}