using System;

namespace Locadora.Domain
{
    public class UserSecurity
    {
        public virtual Int32 Id { get; set; }
        public virtual String Username { get; set; }
        public virtual Boolean IsActive { get; set; }
        public virtual String Client { get; set; }

        public UserSecurity(TUser user)
        {
            this.Id = user.Id;
            this.Username = user.Login;
            this.IsActive = true;
        }

        public UserSecurity(TClient client)
        {
            this.Id = client.Id;
            this.Username = client.Login;
            this.IsActive = true;
        }
    }
}
