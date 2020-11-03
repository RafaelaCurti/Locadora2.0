using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System.Collections.Generic;
using System;

namespace Locadora.Services
{
    public partial interface ITReservationService : IEntityService<TReservation>, IService
    {
        List<TReservation> Search(ReservationSearch reservationSearch);
        Int32 CountSearch(ReservationSearch reservationSearch);
    }
}