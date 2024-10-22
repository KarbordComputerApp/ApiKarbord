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

        public byte? HasChild { get; set; }

        public long? SortAccCode { get; set; }

        public string AccCode { get; set; }

        public string AccName { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        public double? MonBede { get; set; }

        public double? MonBest { get; set; }

        public double? MonTotal { get; set; }

    }
}