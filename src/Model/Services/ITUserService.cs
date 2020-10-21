using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Services
{
    public partial interface ITUserService : IEntityService<TUser>, IService
    {
        TUser FindByUsername(String username);
        Byte[] HashPassword(String password);
        TUser Authenticate(Login login);
        void Edit(TUser model);
    }
}