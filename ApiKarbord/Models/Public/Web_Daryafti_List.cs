namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_Daryafti_List
    {
        [Key]
        public long SerialNumber { get; set; }

        public int BandNo { get; set; }

        public string CheckNo { get; set; }

        public string CheckDate { get; set; }

        public string Bank { get; set; }

        public string Shobe { get; set; }

        public string Jari { get; set; }

        public double? Value { get; set; }

        public string DocDate { get; set; }
    }
}