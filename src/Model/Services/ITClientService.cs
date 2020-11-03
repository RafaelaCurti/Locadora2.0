using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;
using System.Collections.Generic;

namespace Locadora.Services
{
    public partial interface ITClientService : IEntityService<TClient>, IService
    {
        TClient FindByUsername(String username);
        TClient Authenticate(Login login);
        Byte[] HashPassword(String password);
        void Edit(TClient model);
        List<TClient> Search(ClientSearch clientSearch);
        Int32 CountSearch(ClientSearch clientSearch);
    }
}