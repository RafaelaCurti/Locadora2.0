using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using Simple.Entities;
using Locadora.Services;

namespace Locadora.Domain
{
    [Serializable]
    public partial class TBook : Entity<TBook, ITBookService>
    {
        public virtual Int32 Id { get; set; } 

        public virtual String Title { get; set; } 
        public virtual Int32? Pages { get; set; } 
        public virtual String Author { get; set; } 
        public virtual Int32? Edition { get; set; } 
        public virtual Int32? Year { get; set; } 



        #region ' Generated Helpers '
        static TBook()
        {
            Identifiers
                .Add(x => x.Id)
;
        }
        
        partial void Initialize();
        
        public static bool operator ==(TBook obj1, TBook obj2)
        {
            return object.Equals(obj1, obj2);
        }

        public static bool operator !=(TBook obj1, TBook obj2)
        {
            return !(obj1 == obj2);
        }
        
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public TBook() 
        {
            Initialize();
        }
        
        public override TBook Clone()
        {
            var cloned = base.Clone();
            return cloned;
        }

        public TBook(Int32 Id) : this()
        {  
            this.Id = Id;
        }
     
        #endregion

    }
}