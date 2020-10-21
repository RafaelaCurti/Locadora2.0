using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using Simple.Entities;
using Locadora.Services;

namespace Locadora.Domain
{
    public partial class TUser : Entity<TUser, ITUserService>
    {
        public virtual Boolean IsActive { get; set; }
        public virtual String PasswordString { get; set; }
        //public static byte[] HashPassword(byte[] passwordString)
        //{
        //    throw new NotImplementedException();
        //}
    }

}
