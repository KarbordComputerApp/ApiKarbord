namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class TashimBand
    {
        public long SerialNumber { get; set; }

        public bool ForSale { get; set; }

        public int Deghat { get; set; }

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
    }

}