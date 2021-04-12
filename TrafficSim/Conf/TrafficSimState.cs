using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSim.Conf
{
    internal struct TrafficSimState
    {
        public string name { get; set; }
        public string path { get; set; }
        public List<TrafficSimStep> steps { get; set; }
    }
}
