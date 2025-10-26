namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class Web_FDocHMiniApp
    {

        [Key]
        public long SerialNumber { get; set; }

        public string DocNo { get; set; }

        public double? SortDocNo { get; set; }

        public string DocDate { get; set; }

        public string CustCode { get; set; }

        public string CustName { get; set; }

        public string Spec { get; set; }

        public int? KalaPriceCode { get; set; }

        public string InvCode { get; set; }

        public string ModeCode { get; set; }

        public string Status { get; set; }

        public byte? PaymentType { get; set; }

        public string Tanzim { get; set; }

        public string Taeed { get; set; }

        public string Tasvib { get; set; }

        public double? FinalPrice { get; set; }

        public string Eghdam { get; set; }

        public string MkzCode { get; set; }

        public string MkzName { get; set; }

        public string OprCode { get; set; }

        public string OprName { get; set; }

        public string VstrCode { get; set; }

        public string VstrName { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string ArzCode { get; set; }

        public string ArzName { get; set; }

        public double? ArzRate { get; set; }

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

        public bool? RelatedGroupActive { get; set; }

        public string RelatedGroupActiveCap { get; set; }

        public byte? Samane_Status { get; set; }
    }
}