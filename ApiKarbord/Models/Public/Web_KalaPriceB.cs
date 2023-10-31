namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_KalaPriceB
    {
        public int? Code { get; set; }

        [Key]
        public string KalaCode { get; set; }

        public double? Price1 { get; set; }

        public double? Price2 { get; set; }

        public double? Price3 { get; set; }
    }
}