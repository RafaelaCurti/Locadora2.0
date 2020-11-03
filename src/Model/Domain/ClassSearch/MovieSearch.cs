using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Locadora.Domain
{
    public class MovieSearch
    {
        public String name { get; set; }
        public int? category { get; set; }
        public int page { get; set; }
        public int take { get; set; }

        public MovieSearch()
        {
        }
    }
}
