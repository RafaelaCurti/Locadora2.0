using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System.Collections.Generic;
using System;

namespace Locadora.Domain
{
    public partial class TReservation
    {
        public static List<TReservation> Search(ReservationSearch reservationSearch) 
        {
			return Service.Search(reservationSearch);
		}

        public static Int32 CountSearch(ReservationSearch reservationSearch) 
        {
			return Service.CountSearch(reservationSearch);
		}

    }
}