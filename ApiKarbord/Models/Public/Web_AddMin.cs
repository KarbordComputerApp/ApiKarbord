namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_AddMin
    {
        [Key]
        public byte Code { get; set; }

        public string Name { get; set; }

        public int? Mode { get; set; }

        public bool? Auto { get; set; }

        public bool ForSale { get; set; }

        public double? Per { get; set; }

        public bool? DiscountInEffPrice { get; set; }

        public string AddMinInEffPrice1 { get; set; }

        public string AddMinInEffPrice2 { get; set; }

        public string AddMinInEffPrice3 { get; set; }

        public string AddMinInEffPrice4 { get; set; }

        public string AddMinInEffPrice5 { get; set; }

        public string AddMinInEffPrice6 { get; set; }

        public string AddMinInEffPrice7 { get; set; }

        public string AddMinInEffPrice8 { get; set; }

        public string AddMinInEffPrice9 { get; set; }

        public string AddMinInEffPrice10 { get; set; }
    }
}