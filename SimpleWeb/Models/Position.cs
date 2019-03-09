using System;
using System.Collections.Generic;

namespace spiking.Models
{
    public class Position
    {
        public string Grid { get; set; }
        public KeyValuePair<DateTime, double>[] Values { get; set; }
    }
}