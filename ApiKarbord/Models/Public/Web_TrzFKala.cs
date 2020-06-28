namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_TrzFKala
    {
        [Key]
        public string KalaCode { get; set; }

        public string KalaName { get; set; }

        public string KalaF01 { get; set; }

        public string KalaF02 { get; set; }

        public string KalaF03 { get; set; }

        public string KalaF04 { get; set; }

        public string KalaF05 { get; set; }

        public string KalaF06 { get; set; }

        public string KalaF07 { get; set; }

        public string KalaF08 { get; set; }

        public string KalaF09 { get; set; }

        public string KalaF10 { get; set; }

        public string KalaF11 { get; set; }

        public string KalaF12 { get; set; }

        public string KalaF13 { get; set; }

        public string KalaF14 { get; set; }

        public string KalaF15 { get; set; }

        public string KalaF16 { get; set; }

        public string KalaF17 { get; set; }

        public string KalaF18 { get; set; }

        public string KalaF19 { get; set; }

        public string KalaF20 { get; set; }

        public string KalaUnitName1 { get; set; }

        public string KalaUnitName2 { get; set; }

        public string KalaUnitName3 { get; set; }

        public int? DeghatM1 { get; set; }

        public int? DeghatM2 { get; set; }

        public int? DeghatM3 { get; set; }

        public int? DeghatR1 { get; set; }

        public int? DeghatR2 { get; set; }

        public int? DeghatR3 { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double? AddMinPrice1 { get; set; }

        public double? AddMinPrice2 { get; set; }

        public double? AddMinPrice3 { get; set; }

        public double? AddMinPrice4 { get; set; }

        public double? AddMinPrice5 { get; set; }

        public double? AddMinPrice6 { get; set; }

        public double? AddMinPrice7 { get; set; }

        public double? AddMinPrice8 { get; set; }

        public double? AddMinPrice9 { get; set; }

        public double? AddMinPrice10 { get; set; }

        public double? UnitPrice1 { get; set; }

        public double? UnitPrice2 { get; set; }

        public double? UnitPrice3 { get; set; }

        public double? Discount { get; set; }

        public double? NoDiscountPrice { get; set; }

        public double? FinalPrice { get; set; }

        public double? TotalPrice { get; set; }
    }
}