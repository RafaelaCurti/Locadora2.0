using Locadora.Domain;
using Simple;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;

namespace Locadora
{
    public class SecurityContext
    {
        public static SecurityContext Do
        {
            get { return SimpleContext.Data.Singleton<SecurityContext>(); }
        }

        UserSecurity _user = null;
        UserSecurity _client = null;
        bool _isAuthenticated = false;
        Func<IIdentity> _identityGetter = null;

        public void Demand(bool action)
        {
            if (!action) throw new AuthorizationException();
        }

        public bool IsAuthenticated { get { return _isAuthenticated; } }

        public UserSecurity User { get { return _user; } }
        public SecurityContext Init(Func<IIdentity> identityGetter, bool isAdmin)
        {
            _identityGetter = identityGetter;
            _isAuthenticated = TryGet(x => x.IsAuthenticated, false);

            var username = TryCast(x => x.Name, string.Empty);
            if (_isAuthenticated && !string.IsNullOrEmpty(username))
            {
                if (isAdmin)
                {
                    var model = TUser.FindByUsername(username);
                    if (model != null)
                        _user = new UserSecurity(model);
                }
                else 
                {
                    var model = TClient.FindByUsername(username);
                    if (model != null)
                        _user = new UserSecurity(model);
                }
            }
            else
                _user = null;

            // VERIFICAR COM MATHEUS SE É NECESSÁRIO. POIS SEMPRE QUE ESTIVERMOS NA ÁREA DE USUÁRIOS, A AUTENTICAÇÃO SERÁ FALSE E COM ISSO NÃO CONSEGUIREMOS ACESSAR AS OPÇÕES
            if (_user == null)
                _isAuthenticated = false;

            return this;
        }

        protected V TryCast<T, V>(Func<IIdentity, T> attr, V def)
            where V : IConvertible
            where T : class, IConvertible
        {
            var obj = TryGet<T>(attr, null);
            if (obj == null) return def;
            try
            {
                return (V)Convert.ChangeType(obj, typeof(V));
            }
            catch (FormatException)
            {
                return def;
            }
        }

        protected T TryGet<T>(Func<IIdentity, T> attr, T def)
        {
            try
            {
                return attr(_identityGetter());
            }
            catch (NullReferenceException)
            {
                return def;
            }
        }
    }
}
