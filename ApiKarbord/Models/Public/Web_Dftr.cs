namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_Dftr
    {

        public long? SerialNumber { get; set; }

        public byte? BodyTag { get; set; }

        public long? DocNo { get; set; }

        [StringLength(10)]
        public string DocDate { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(10)]
        public string Eghdam { get; set; }

        [StringLength(10)]
        public string Tanzim { get; set; }

        [StringLength(10)]
        public string Taeed { get; set; }

        [StringLength(10)]
        public string Tasvib { get; set; }

        [StringLength(30)]
        public string ModeCode { get; set; }

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

        [Key]
        public int BandNo { get; set; }


        public string SortAccCode { get; set; }

        [StringLength(50)]
        public string AccCode { get; set; }

        [StringLength(100)]
        public string AccName { get; set; }


        public string SortMkzCode { get; set; }

        [StringLength(50)]
        public string MkzCode { get; set; }

        [StringLength(250)]
        public string MkzName { get; set; }

       
        [StringLength(50)]
        public string OprCode { get; set; }

        [StringLength(100)]
        public string OprName { get; set; }

        [StringLength(4000)]
        public string Comm { get; set; }

        [StringLength(50)]
        public string ArzCode { get; set; }

        public double ArzRate { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        public double? Amount { get; set; }

        public double? ArzValue { get; set; }

        //public double? SumBede { get; set; }

        //public double? SumBest { get; set; }

        public double? MonTotal { get; set; }

        public double? MonBede { get; set; }

        public double? MonBest { get; set; }

        [StringLength(30)]
        public string CheckNo { get; set; }

        [StringLength(10)]
        public string CheckDate { get; set; }

        [StringLength(100)]
        public string Bank { get; set; }

        [StringLength(100)]
        public string Shobe { get; set; }

        [StringLength(100)]
        public string Jari { get; set; }

        [StringLength(50)]
        public string TrafCode { get; set; }

        [StringLength(100)]
        public string TrafName { get; set; }

        [StringLength(100)]
        public string ModeName { get; set; }

    }
}