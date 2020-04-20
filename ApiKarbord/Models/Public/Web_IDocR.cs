namespace ApiKarbord.Models
 {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class Web_IDocR
    {
        [Key]
        public long SerialNumber { get; set; }

        public int DocNo { get; set; }

        public int BandNo { get; set; }

        [StringLength(250)]
        public string ModeName { get; set; }

        [StringLength(250)]
        public string InvName { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(10)]
        public string Taeed { get; set; }

        [StringLength(10)]
        public string Tasvib { get; set; }

        [StringLength(100)]
        public string ThviName { get; set; }

        [StringLength(250)]
        public string MkzName { get; set; }

        [StringLength(250)]
        public string OprName { get; set; }

        [StringLength(250)]
        public string KalaName { get; set; }

        [StringLength(50)]
        public string KalaFileNo { get; set; }

        [StringLength(50)]
        public string KalaState { get; set; }

        [StringLength(50)]
        public string KalaExf1 { get; set; }

        [StringLength(50)]
        public string KalaExf2 { get; set; }

        [StringLength(50)]
        public string KalaExf3 { get; set; }

        [StringLength(50)]
        public string MainUnitName { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double UnitPrice { get; set; }

        public double TotalPrice { get; set; }

        [StringLength(250)]
        public string BandSpec { get; set; }

        [StringLength(250)]
        public string Comm { get; set; }

    }
}
