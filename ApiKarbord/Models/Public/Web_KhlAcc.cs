namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_KhlAcc
    {
        [Key]
        public int Tag { get; set; }

        public long? SortAccCode { get; set; }

        [Key]
        public string AccCode { get; set; }

        [Key]
        public string AccName { get; set; }

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