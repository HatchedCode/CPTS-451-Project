using System;

namespace YelpEngine
{
    internal class Business
    {
        public string name { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string bid { get; set; }
        public int stars { get; set; }
        public int rev_count { get; set; }
        public float longitude { get; set; }
        public string address { get; set; }
        public float latitude { get; set; }
        public int isOpen { get; set; }
        public int numCheckins { get; set; }
        public int numTips { get; set; }
        public float distance { get; set; }
    } 
}
