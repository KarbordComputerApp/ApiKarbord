namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_FDocH
    {


        public string DocNo { get; set; }

        public double? SortDocNo { get; set; }

        //10)]
        //public string Tasvib { get; set; }

        public string ModeCode { get; set; }

        public double? FinalPrice { get; set; }

        public string PaymentTypeSt { get; set; }

        [Key]
        public long SerialNumber { get; set; }

        public string DocDate { get; set; }

        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Spec { get; set; }

        public string Status { get; set; }

        public string Tanzim { get; set; }

        public string Taeed { get; set; }

        public string TahieShode { get; set; }

        public string Footer { get; set; }

        public byte? PaymentType { get; set; }

        public string CustCode { get; set; }

        public string F01 { get; set; }

        public string F02 { get; set; }

        public string F03 { get; set; }

        public string F04 { get; set; }

        public string F05 { get; set; }

        public string F06 { get; set; }

        public string F07 { get; set; }

        public string F08 { get; set; }

        public string F09 { get; set; }

        public string F10 { get; set; }

        public string F11 { get; set; }

        public string F12 { get; set; }

        public string F13 { get; set; }

        public string F14 { get; set; }

        public string F15 { get; set; }

        public string F16 { get; set; }

        public string F17 { get; set; }

        public string F18 { get; set; }

        public string F19 { get; set; }

        public string F20 { get; set; }

        public string AddMinSpec1 { get; set; }

        public string AddMinSpec2 { get; set; }

        public string AddMinSpec3 { get; set; }

        public string AddMinSpec4 { get; set; }

        public string AddMinSpec5 { get; set; }

        public string AddMinSpec6 { get; set; }

        public string AddMinSpec7 { get; set; }

        public string AddMinSpec8 { get; set; }

        public string AddMinSpec9 { get; set; }

        public string AddMinSpec10 { get; set; }

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

        public string InvCode { get; set; }

        public int? KalaPriceCode { get; set; }

        public string Eghdam { get; set; }

        public DateTime? EghdamDate { get; set; }

        public string CustName { get; set; }

        public string VstrName { get; set; }

        public string VstrCode { get; set; }

        public string MkzCode { get; set; }

        public string MkzName { get; set; }

        public string OprCode { get; set; }

        public string OprName { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double? TotalPrice { get; set; }

        public double? Discount { get; set; }

        public string ArzCode { get; set; }

        public string ArzName { get; set; }

        public double? ArzRate { get; set; }

        public long? AccSerialNumber { get; set; }

        public int? AccDocNo { get; set; }

        public bool? RelatedGroupActive { get; set; }

        public string RelatedGroupActiveCap { get; set; }

        public byte? Samane_Status { get; set; }

    }
}