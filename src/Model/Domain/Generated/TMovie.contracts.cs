using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System.Collections.Generic;
using System;

namespace Locadora.Domain
{
    public partial class TMovie
    {
        public static List<TMovie> Search(MovieSearch movieSearch) 
        {
			return Service.Search(movieSearch);
		}

        public static Int32 CountSearch(MovieSearch movieSearch) 
        {
			return Service.CountSearch(movieSearch);
		}

    }
}