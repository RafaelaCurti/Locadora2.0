﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Locadora.Domain
{
    public partial class TReservation
    {
        public virtual Int32 IdClient { get; set; }
        ////public virtual List<TMovie> Iten { get; set; }
        public virtual StatusSale Sales { get; set; }
        public virtual int[] Itens { get; set; }
        public virtual int[] Quantities { get; set; }
        //public static List<TReservation> Search(ReservationSearch reservationSearch)
        //{
        //    return Service.Search(reservationSearch);
        //}
        //public static Int32 CountSearch(ReservationSearch reservationSearch)
        //{
        //    return Service.CountSearch(reservationSearch);
        //}

    }

}