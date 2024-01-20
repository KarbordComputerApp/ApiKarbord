using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiKarbord.Models
{
    public class LocationGeocoding
    {
        [Key]
        public double x { get; set; }
        [Key]
        public double y { get; set; }
    }

    public class Geocoding
    {
        public LocationGeocoding location { get; set; }
        public string status { get; set; }
    }
}