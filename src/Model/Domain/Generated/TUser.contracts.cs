using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Domain
{
    public partial class TUser
    {
        public static TUser FindByUsername(String username) 
        {
			return Service.FindByUsername(username);
		}

        public static Byte[] HashPassword(String password) 
        {
			return Service.HashPassword(password);
		}

        public static TUser Authenticate(Login login) 
        {
			return Service.Authenticate(login);
		}

        public virtual void Edit() 
        {
			Service.Edit(this);
		}

    }
}