using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using System.Security.Cryptography;
using NHibernate.Criterion;

namespace Locadora.Services
{
    public partial class TClientService : EntityService<TClient>, ITClientService
    {

        public TClient FindByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            return Session.CreateCriteria<TClient>()
                .Add(Restrictions.Eq("Login", username))
                .SetMaxResults(1)
                .UniqueResult<TClient>();
        }

        public TClient Authenticate(Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Client) || string.IsNullOrWhiteSpace(login.Password))
                return null;

            var client = Session.CreateCriteria<TClient>()
                .Add(Restrictions.Eq("Login", login.Client))
                .SetMaxResults(1)
                .UniqueResult<TClient>();
            if (client == null ||client.Password == null)
                return null;

            var hashTryPassword = HashPassword(login.Password);
            return client.Password.SequenceEqual(hashTryPassword) ? client : null;
        }
        public byte[] HashPassword(string password)
        {
            return SHA384.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public void Edit(TClient model)
        {
            var client = TClient.Load(model.Id);
            client.Name = model.Name;
            client.Email = model.Email;
            client.Telephone = model.Telephone;
            client.Login = model.Login;
            client.Update();
        }
    }
}