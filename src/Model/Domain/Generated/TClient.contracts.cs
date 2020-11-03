using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;
using System.Collections.Generic;

namespace Locadora.Domain
{
    public partial class TClient
    {
        public static TClient FindByUsername(String username) 
        {
			return Service.FindByUsername(username);
		}

        public static TClient Authenticate(Login login) 
        {
			return Service.Authenticate(login);
		}

        public static Byte[] HashPassword(String password) 
        {
			return Service.HashPassword(password);
		}

        public virtual void Edit() 
        {
			Service.Edit(this);
		}

        public static List<TClient> Search(ClientSearch clientSearch) 
        {
			return Service.Search(clientSearch);
		}

        public static Int32 CountSearch(ClientSearch clientSearch) 
        {
			return Service.CountSearch(clientSearch);
		}

    }
}