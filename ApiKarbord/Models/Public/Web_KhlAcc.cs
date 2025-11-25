namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_KhlAcc
    {
        public short? Tag { get; set; }

        public byte? HasChild { get; set; }

        public long? SortAccCode { get; set; }

        public string AccCode { get; set; }

        [Key]
        public string AccName { get; set; }

        public string AccF01 { get; set; }

        public string AccF02 { get; set; }

        public string AccF03 { get; set; }

        public string AccF04 { get; set; }

        public string AccF05 { get; set; }

        public string AccF06 { get; set; }

        public string AccF07 { get; set; }

        public string AccF08 { get; set; }

        public string AccF09 { get; set; }

        public string AccF10 { get; set; }

        public string AccF11 { get; set; }

        public string AccF12 { get; set; }

        public string AccF13 { get; set; }

        public string AccF14 { get; set; }

        public string AccF15 { get; set; }

        public string AccF16 { get; set; }

        public string AccF17 { get; set; }

        public string AccF18 { get; set; }

        public string AccF19 { get; set; }

        public string AccF20 { get; set; }

        public long? SortMkzCode { get; set; }

        [Key]
        public string MkzCode { get; set; }

        [Key]
        public string MkzName { get; set; }

        [Key]
        public string OprCode { get; set; }

        [Key]
        public string OprName { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        public double? MonBede { get; set; }

        public double? MonBest { get; set; }

        [Key]
        public double MonTotal { get; set; }

    }
}