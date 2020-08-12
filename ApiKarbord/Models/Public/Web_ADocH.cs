namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_ADocH
    {
        
        public byte? Balance { get; set; }

        public long? SerialNumber { get; set; }

        public long? DocNo { get; set; }

        public string DocDate { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(10)]
        public string Tanzim { get; set; }

        [StringLength(10)]
        public string Taeed { get; set; }

        [StringLength(10)]
        public string Eghdam { get; set; }

        [StringLength(10)]
        public string Tasvib { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(30)]
        public string ModeCode { get; set; }

        [Key]
        public string ModeName { get; set; }

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

        public double? SortDocNo { get; set; }
    }
}