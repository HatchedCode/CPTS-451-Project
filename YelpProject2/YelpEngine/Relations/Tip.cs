using System;
using System.Collections.Generic;
using System.Text;

namespace YelpEngine
{
    public class Tip
    {
        public string user_name { get; set; }
        public string bus_name { get; set; }
        public string bus_city { get; set; }

        public string business_id { get; set; }
        public string user_id { get; set; }
        public string likes { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
    }
}
