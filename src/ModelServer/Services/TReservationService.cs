using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using NHibernate.Criterion;
using NHibernate;
using MoreLinq;
using NHibernate.SqlCommand;

namespace Locadora.Services
{
    public partial class TReservationService : EntityService<TReservation>, ITReservationService
    {
        public List<TReservation> Search(ReservationSearch reservationSearch)
        {
            var criteria = SearchCriteria(reservationSearch);
            criteria.AddOrder(Order.Asc("Id"));
            criteria.SetFirstResult((reservationSearch.page - 1) * reservationSearch.take).SetMaxResults(reservationSearch.take);
            return criteria.List<TReservation>().DistinctBy(x => x.Id).ToList();
        }

        public int CountSearch(ReservationSearch reservationSearch)
        {
            var criteria = SearchCriteria(reservationSearch);
            return criteria.SetProjection(Projections.CountDistinct("Id")).UniqueResult<int>();
        }
        private ICriteria SearchCriteria(ReservationSearch reservationSearch)
        {
            var criteria = Session.CreateCriteria<TReservation>()
               //.Add(Restrictions.Between(dateType, reservationSearch.startDate.ToInitialDate(), reservationSearch.endDate.ToEndDate()))
            //   criteria.CreateAlias("TOrderItems", "productsItems", JoinType.InnerJoin);
            //criteria.Add(Restrictions.In("productsItems.Product.Id", orderSearch.products));
               .CreateAlias("Client", "client", JoinType.LeftOuterJoin)
               .SetFetchMode("client", FetchMode.Eager);
            if(!string.IsNullOrWhiteSpace(reservationSearch.client))
                criteria.Add(Restrictions.Eq("client.Name", reservationSearch.client.Trim()));
            if (reservationSearch.itens.HasValue)
            {
                criteria.CreateAlias("TItens", "itens", JoinType.LeftOuterJoin);
                criteria.Add(Restrictions.Eq("itens.Movie.Id", reservationSearch.itens.Value));
            }

            return criteria;
        }
        private ICriteria SearchByReservationCriteria(ReservationSearch reservationSearch)
        {
            var criteria = Session.CreateCriteria<TReservation>()
                .Add(Restrictions.IsNotNull("Client"))
                .Add(Restrictions.Eq("Client.Id", reservationSearch.client));
            return criteria;
        }
    }
}