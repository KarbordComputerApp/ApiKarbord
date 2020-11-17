namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_IDocB
    {
        [Key]
        public long SerialNumber { get; set; }

        public int BandNo { get; set; }

        [StringLength(100)]
        public string KalaCode { get; set; }

        [StringLength(250)]
        public string KalaName { get; set; }

        public Int16? MainUnit { get; set; }

        public string MainUnitName { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double? UnitPrice { get; set; }

        public double? TotalPrice { get; set; }

        public string Comm { get; set; }

        public bool UP_Flag { get; set; }

        public byte? KalaDeghatM1 { get; set; }

        public byte? KalaDeghatM2 { get; set; }

        public byte? KalaDeghatM3 { get; set; }

        public byte? KalaDeghatR1 { get; set; }

        public byte? KalaDeghatR2 { get; set; }

        public byte? KalaDeghatR3 { get; set; }

        public int? DeghatR { get; set; }
    }
}