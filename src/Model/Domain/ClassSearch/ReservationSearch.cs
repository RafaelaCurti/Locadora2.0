using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Locadora.Domain
{
    public class ReservationSearch
    {
        public String client { get; set; }
        public int? itens { get; set; }
        public DateTime? withdraw { get; set; }
        public DateTime? devolution { get; set; }
        public int page { get; set; }
        public int take { get; set; }

        public ReservationSearch()
        {
        }
    }
}
