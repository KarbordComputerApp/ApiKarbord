namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_TrzFCust
    {
        [Key]
        public string CustCode { get; set; }

        public string CustName { get; set; }

        public string CGruCode { get; set; }

        public string CustF01 { get; set; }

        public string CustF02 { get; set; }

        public string CustF03 { get; set; }

        public string CustF04 { get; set; }

        public string CustF05 { get; set; }

        public string CustF06 { get; set; }

        public string CustF07 { get; set; }

        public string CustF08 { get; set; }

        public string CustF09 { get; set; }

        public string CustF10 { get; set; }

        public string CustF11 { get; set; }

        public string CustF12 { get; set; }

        public string CustF13 { get; set; }

        public string CustF14 { get; set; }

        public string CustF15 { get; set; }

        public string CustF16 { get; set; }

        public string CustF17 { get; set; }

        public string CustF18 { get; set; }

        public string CustF19 { get; set; }

        public string CustF20 { get; set; }

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

        public double? Discount { get; set; }

        public double? OnlyDiscountPrice { get; set; }

        public double? FinalPrice { get; set; }

        public double? TotalPrice { get; set; }

        public double? AccBede { get; set; }

        public double? AccBest { get; set; }

        public double? AccMon { get; set; }

    }
}