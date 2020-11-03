using System;
//using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using System.Security.Cryptography;
using NHibernate.Criterion;
using NHibernate;
using MoreLinq;
using NHibernate.SqlCommand;
using Locadora.Helpers;

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
            client.Preference = model.Preference;
            client.Update();
        }

        public List<TClient> Search(ClientSearch clientSearch)
        {
            var criteria = SearchCriteria(clientSearch);
            criteria.AddOrder(Order.Asc("Id"));
            criteria.SetFirstResult((clientSearch.page - 1) * clientSearch.take).SetMaxResults(clientSearch.take);
            return criteria.List<TClient>().DistinctBy(x => x.Id).ToList();
        }
        
        public int CountSearch(ClientSearch clientSearch)
        {
            var criteria = SearchCriteria(clientSearch);
            return criteria.SetProjection(Projections.CountDistinct("Id")).UniqueResult<int>();
        }
        private ICriteria SearchCriteria(ClientSearch clientSearch)
        {
            var criteria = Session.CreateCriteria<TClient>();

            if (!string.IsNullOrWhiteSpace(clientSearch.name))
                criteria.Add(Expression.Sql("{alias}.name like ? COLLATE Latin1_General_CI_AI", string.Format("%{0}%", clientSearch.name), NHibernateUtil.String));

            if (!string.IsNullOrWhiteSpace(clientSearch.email))
                criteria.Add(Restrictions.InsensitiveLike("Email", string.Format("%{0}%", clientSearch.email.Trim())));

            if (!string.IsNullOrWhiteSpace(clientSearch.username))
                criteria.Add(Restrictions.InsensitiveLike("Login", string.Format("%{0}%", clientSearch.username.Trim())));
            if (clientSearch.preference.HasValue)
            {
                criteria.CreateAlias("TCategory", "preference", JoinType.LeftOuterJoin);
                criteria.Add(Restrictions.Eq("category.Name", clientSearch.preference.Value));
            }

            return criteria;
        }

    }
}