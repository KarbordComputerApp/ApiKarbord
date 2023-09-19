namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class CalcAddmin
    {
        public bool forSale { get; set; }

        public long serialNumber { get; set; }

        public string custCode { get; set; }

        public byte typeJob { get; set; }
        public string spec1 { get; set; }
        public string spec2 { get; set; }
        public string spec3 { get; set; }
        public string spec4 { get; set; }
        public string spec5 { get; set; }
        public string spec6 { get; set; }
        public string spec7 { get; set; }
        public string spec8 { get; set; }
        public string spec9 { get; set; }
        public string spec10 { get; set; }

        public double? MP1 { get; set; }
        public double? MP2 { get; set; }
        public double? MP3 { get; set; }
        public double? MP4 { get; set; }
        public double? MP5 { get; set; }
        public double? MP6 { get; set; }
        public double? MP7 { get; set; }
        public double? MP8 { get; set; }
        public double? MP9 { get; set; }
        public double? MP10 { get; set; }

        public string flagTest { get; set; }
    }

}