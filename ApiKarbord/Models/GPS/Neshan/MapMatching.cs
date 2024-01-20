using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiKarbord.Models
{
    public partial class LocationMapMatching
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class MapMatching
    {
        public List<SnappedPoint> snappedPoints { get; set; }
        public string geometry { get; set; }
    }

    public class SnappedPoint
    {
        public LocationMapMatching location { get; set; }
        public int originalIndex { get; set; }
    }
}