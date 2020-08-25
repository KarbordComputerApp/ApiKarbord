namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_IDocHMini
    {
        [Key]
        public long SerialNumber { get; set; }

        public int? InOut { get; set; }

        public string DocNo { get; set; }

        public double? SortDocNo { get; set; }

        [StringLength(10)]
        public string DocDate { get; set; }


        [StringLength(50)]
        public string ThvlCode { get; set; }


        [StringLength(100)]
        public string thvlname { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        public int? KalaPriceCode { get; set; }

        [StringLength(20)]
        public string InvCode { get; set; }

        public string ModeCode { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        public byte PaymentType { get; set; }

        [StringLength(4000)]
        public string Footer { get; set; }


        [StringLength(10)]
        public string Tanzim { get; set; }


        [StringLength(10)]
        public string Taeed { get; set; }

        public double? FinalPrice { get; set; }

        [StringLength(20)]
        public string Eghdam { get; set; }

        public string ModeName { get; set; }

        public string InvName { get; set; }

        [StringLength(250)]
        public string F01 { get; set; }


        [StringLength(250)]
        public string F02 { get; set; }


        [StringLength(250)]
        public string F03 { get; set; }

        [StringLength(250)]
        public string F04 { get; set; }


        [StringLength(250)]
        public string F05 { get; set; }

        [StringLength(250)]
        public string F06 { get; set; }


        [StringLength(250)]
        public string F07 { get; set; }

        [StringLength(250)]
        public string F08 { get; set; }


        [StringLength(250)]
        public string F09 { get; set; }

        [StringLength(250)]
        public string F10 { get; set; }


        [StringLength(250)]
        public string F11 { get; set; }

        [StringLength(250)]
        public string F12 { get; set; }

        [StringLength(250)]
        public string F13 { get; set; }

        [StringLength(250)]
        public string F14 { get; set; }


        [StringLength(250)]
        public string F15 { get; set; }

        [StringLength(250)]
        public string F16 { get; set; }


        [StringLength(250)]
        public string F17 { get; set; }

        [StringLength(250)]
        public string F18 { get; set; }


        [StringLength(250)]
        public string F19 { get; set; }

        [StringLength(250)]
        public string F20 { get; set; }

    }
}