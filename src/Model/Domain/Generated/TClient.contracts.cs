using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

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

    }
}