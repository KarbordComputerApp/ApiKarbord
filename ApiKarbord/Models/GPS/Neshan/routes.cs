using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiKarbord.Models
{
    public class Distance
    {
        public double value { get; set; }
        public string text { get; set; }
    }

    public class Duration
    {
        public double value { get; set; }
        public string text { get; set; }
    }

    public class Leg
    {
        public string summary { get; set; }
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public List<Step> steps { get; set; }
    }

    public class OverviewPolyline
    {
        public string points { get; set; }
    }

    public class Root
    {
        public List<Route> routes { get; set; }
    }

    public class Route
    {
        public OverviewPolyline overview_polyline { get; set; }
        public List<Leg> legs { get; set; }
    }

    public class Step
    {
        public string name { get; set; }
        public string instruction { get; set; }
        public int bearing_after { get; set; }
        public string type { get; set; }
        public string modifier { get; set; }
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string polyline { get; set; }
        public List<double> start_location { get; set; }
        public int? exit { get; set; }
    }
}