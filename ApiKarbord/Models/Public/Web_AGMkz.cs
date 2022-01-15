namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_AGMkz
    {
        public short? MainLevel { get; set; }

        public byte? Tag { get; set; }

        public short? Level { get; set; }

        public long? SortMkzCode { get; set; }

        public string MkzCode { get; set; }

        public string MkzName { get; set; }

        public string AccCode { get; set; }

        public string AccName { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        public double? MonBede { get; set; }

        public double? MonBest { get; set; }

        public double? MonTotal { get; set; }
    }
}