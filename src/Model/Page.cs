using System;

namespace Locadora.Domain
{
    public class Page
    {
        public virtual int TotalItems { get; set; }
        public virtual int CurrentPage { get; set; }
        public virtual int ItemsPerPage { get; set; }
        public virtual int TotalPages
        {
            get
            {
                try
                {
                    if (this.TotalItems == 0)
                        return 0;
                    if (this.TotalItems == this.ItemsPerPage)
                        return 1;
                    var division = (double)this.TotalItems / (double)this.ItemsPerPage;
                    if (division % 2 == 0)
                        return (int)division;
                    return (int)division + 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
