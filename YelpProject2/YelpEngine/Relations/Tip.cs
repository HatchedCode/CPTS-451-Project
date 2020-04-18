using System;
using System.Collections.Generic;
using System.Text;

namespace YelpEngine
{
    internal class Tip
    {
        public string business_id { get; set; }
        public string user_id { get; set; }
        public string likes { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
    }
}
