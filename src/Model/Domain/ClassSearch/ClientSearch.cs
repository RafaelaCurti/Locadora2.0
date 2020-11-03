using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Locadora.Domain
{
    public class ClientSearch
    {
        public String name { get; set; }
        public String username { get; set; }
        public String email { get; set; }
        public int? preference { get; set; }
        public String phone { get; set; }
        public int page { get; set; }
        public int take { get; set; }

        public ClientSearch()
        {
        }
    }
}
