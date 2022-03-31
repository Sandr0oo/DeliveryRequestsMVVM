using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestDelivery
{
    public class Utility
    {
        public static EventAggregator EventAggregator { get; set; }

        static Utility()
        {
            EventAggregator = new EventAggregator();
        }
    }
}
