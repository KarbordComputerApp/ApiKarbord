namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_TrzAcc
    {

        public short? MainLevel { get; set; }

        public short? Level { get; set; }

        [StringLength(50)]
        public string AccCode { get; set; }

        public string SortAccCode { get; set; }

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

        public double? Bede { get; set; }

        public double? Best { get; set; }

        public double? MonBede { get; set; }

        public double? MonBest { get; set; }

        public double? MonTotal { get; set; }

    }
}