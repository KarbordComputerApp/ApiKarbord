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


        public long? SortAccCode { get; set; }


        public string AccCode { get; set; }


        public string MkzCode { get; set; }

        public long? SortMkzCode { get; set; }


        public string OprCode { get; set; }


        public string Comm { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        public double? Amount { get; set; }


        public string ArzCode { get; set; }

        public double? ArzValue { get; set; }

        public double? ArzRate { get; set; }

        public long? SerialNumber { get; set; }

        public long? DocNo { get; set; }


        public string DocDate { get; set; }


        public string Spec { get; set; }


        public string Status { get; set; }


        public string Tanzim { get; set; }


        public string Taeed { get; set; }


        public string Eghdam { get; set; }


        public string Tasvib { get; set; }


        public string ModeCode { get; set; }


        public string F01 { get; set; }


        public string F02 { get; set; }


        public string F03 { get; set; }


        public string F04 { get; set; }


        public string F05 { get; set; }


        public string F06 { get; set; }


        public string F07 { get; set; }


        public string F08 { get; set; }


        public string F09 { get; set; }


        public string F10 { get; set; }


        public string F11 { get; set; }


        public string F12 { get; set; }


        public string F13 { get; set; }


        public string F14 { get; set; }


        public string F15 { get; set; }


        public string F16 { get; set; }


        public string F17 { get; set; }


        public string F18 { get; set; }


        public string F19 { get; set; }


        public string F20 { get; set; }

        [Key]

        public string AccName { get; set; }

       
        [Key]

        public string ArzName { get; set; }

        [Key]

        public string TrafName { get; set; }

        [Key]

        public string MkzName { get; set; }

        [Key]

        public string OprName { get; set; }


        public string CheckNo { get; set; }


        public string CheckDate { get; set; }


        public string Bank { get; set; }


        public string Shobe { get; set; }


        public string Jari { get; set; }


        public string TrafCode { get; set; }

        [Key]
        public string ModeName { get; set; }

        public double? ArzBede { get; set; }

        public double? ArzBest { get; set; }
    }
}