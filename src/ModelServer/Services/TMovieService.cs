using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using NHibernate.Criterion;
using NHibernate;
using NHibernate.SqlCommand;
using MoreLinq;

namespace Locadora.Services
{
    public partial class TMovieService : EntityService<TMovie>, ITMovieService
    {
        public List<TMovie> Search(MovieSearch movieSearch)
        {
            var criteria = SearchCriteria(movieSearch);
            criteria.AddOrder(Order.Asc("Id"));
            criteria.SetFirstResult((movieSearch.page - 1) * movieSearch.take).SetMaxResults(movieSearch.take);
            return criteria.List<TMovie>().DistinctBy(x => x.Id).ToList();
        }


        public int CountSearch(MovieSearch movieSearch)
        {
            var criteria = SearchCriteria(movieSearch);
            return criteria.SetProjection(Projections.CountDistinct("Id")).UniqueResult<int>();
        }
        private ICriteria SearchCriteria(MovieSearch movieSearch)
        {
            var criteria = Session.CreateCriteria<TMovie>();
               //.Add(Restrictions.Between(dateType, reservationSearch.startDate.ToInitialDate(), reservationSearch.endDate.ToEndDate()))
               //   criteria.CreateAlias("TOrderItems", "productsItems", JoinType.InnerJoin);
               //criteria.Add(Restrictions.In("productsItems.Product.Id", orderSearch.products));
               //.CreateAlias("Name", "name", JoinType.LeftOuterJoin)
               //.SetFetchMode("name", FetchMode.Eager);

            if (!string.IsNullOrWhiteSpace(movieSearch.name))
                criteria.Add(Expression.Sql("{alias}.name like ? COLLATE Latin1_General_CI_AI", string.Format("%{0}%", movieSearch.name), NHibernateUtil.String));
            if (movieSearch.category.HasValue)
            {
                criteria.CreateAlias("TMovieCategory", "group", JoinType.LeftOuterJoin);
                criteria.Add(Restrictions.Eq("group.Category.Id", movieSearch.category.Value));
                //Chamei de group pois ficaria dif�cil de entender se chamasse o apelido da Tabela TMmovieCategory de category
            }

            return criteria;
        }
        //private ICriteria SearchByMovieCriteria(MovieSearch movieSearch)
        //{
        //    var criteria = Session.CreateCriteria<TMovie>()
        //        .Add(Restrictions.IsNotNull("Movie"))
        //        .Add(Restrictions.Eq("Name.Id", movieSearch.name));
        //    return criteria;
        //}
    }
}
