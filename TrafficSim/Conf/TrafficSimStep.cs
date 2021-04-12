using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSim.Conf
{
    internal struct TrafficSimStep
    {
        public string action { get; set; }
        public string to { get; set; }
        public int weight { get; set; }
    }
}
