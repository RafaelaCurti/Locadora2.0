using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System.Collections.Generic;
using System;

namespace Locadora.Services
{
    public partial interface ITMovieService : IEntityService<TMovie>, IService
    {
        List<TMovie> Search(MovieSearch movieSearch);
        Int32 CountSearch(MovieSearch movieSearch);
    }
}