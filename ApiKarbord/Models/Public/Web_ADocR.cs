namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_ADocR
    {
        [Key]
        public int BandNo { get; set; }

        [StringLength(50)]
        public string AccCode { get; set; }

        [StringLength(50)]
        public string MkzCode { get; set; }

        [StringLength(50)]
        public string OprCode { get; set; }

        [StringLength(4000)]
        public string Comm { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        public double? Amount { get; set; }

        [StringLength(50)]
        public string ArzCode { get; set; }

        public double? ArzValue { get; set; }

        public double? ArzRate { get; set; }

        public long? SerialNumber { get; set; }

        public int? DocNo { get; set; }

        [StringLength(10)]
        public string DocDate { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(10)]
        public string Tanzim { get; set; }

        [StringLength(10)]
        public string Taeed { get; set; }

        [StringLength(10)]
        public string Eghdam { get; set; }

        [StringLength(10)]
        public string Tasvib { get; set; }

        [StringLength(30)]
        public string ModeCode { get; set; }

        [StringLength(50)]
        public string F01 { get; set; }

        [StringLength(50)]
        public string F02 { get; set; }

        [StringLength(50)]
        public string F03 { get; set; }

        [StringLength(50)]
        public string F04 { get; set; }

        [StringLength(50)]
        public string F05 { get; set; }

        [StringLength(50)]
        public string F06 { get; set; }

        [StringLength(50)]
        public string F07 { get; set; }

        [StringLength(50)]
        public string F08 { get; set; }

        [StringLength(50)]
        public string F09 { get; set; }

        [StringLength(50)]
        public string F10 { get; set; }

        [StringLength(50)]
        public string F11 { get; set; }

        [StringLength(50)]
        public string F12 { get; set; }

        [StringLength(50)]
        public string F13 { get; set; }

        [StringLength(50)]
        public string F14 { get; set; }

        [StringLength(50)]
        public string F15 { get; set; }

        [StringLength(50)]
        public string F16 { get; set; }

        [StringLength(50)]
        public string F17 { get; set; }

        [StringLength(50)]
        public string F18 { get; set; }

        [StringLength(50)]
        public string F19 { get; set; }

        [StringLength(50)]
        public string F20 { get; set; }

        [Key]
        [StringLength(100)]
        public string AccName { get; set; }

        [StringLength(9)]
        public string AccCode1 { get; set; }

        [StringLength(9)]
        public string AccCode2 { get; set; }

        [StringLength(9)]
        public string AccCode3 { get; set; }

        [StringLength(9)]
        public string AccCode4 { get; set; }

        [StringLength(9)]
        public string AccCode5 { get; set; }

        [Key]
        [StringLength(50)]
        public string ArzName { get; set; }

        [Key]
        [StringLength(100)]
        public string TrafName { get; set; }

        [Key]
        [StringLength(250)]
        public string MkzName { get; set; }

        [Key]
        [StringLength(100)]
        public string OprName { get; set; }

        [Key]
        public string ModeName { get; set; }

        public double? ArzBede { get; set; }

        public double? ArzBest { get; set; }
    }
}