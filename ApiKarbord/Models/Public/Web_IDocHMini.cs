namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_IDocHMini
    {
        [Key]
        public long SerialNumber { get; set; }

        public int? InOut { get; set; }

        public string DocNo { get; set; }

        public double? SortDocNo { get; set; }

        public string DocDate { get; set; }

        public string ThvlCode { get; set; }

        public string ThvlName { get; set; }

        public string Spec { get; set; }

        public int? KalaPriceCode { get; set; }

        public string InvCode { get; set; }

        public string ModeCode { get; set; }

        public string Status { get; set; }

        public byte PaymentType { get; set; }

        public string Footer { get; set; }

        public string Tanzim { get; set; }

        public string Taeed { get; set; }

        public string Tasvib { get; set; }

        public double? FinalPrice { get; set; }

        public string Eghdam { get; set; }

        public string ModeName { get; set; }

        public string InvName { get; set; }

        public DateTime? UpdateDate { get; set; }

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

        public string MkzCode { get; set; }

        public string MkzName { get; set; }

        public string OprCode { get; set; }

        public string OprName { get; set; }

        public string ThvlRegion { get; set; }

        public string ThvlCity { get; set; }

        public string ThvlStreet { get; set; }

        public string ThvlAlley { get; set; }

        public string ThvlPlack { get; set; }

        public string ThvlZipCode { get; set; }

        public string ThvlTel { get; set; }

        public string ThvlMobile { get; set; }

        public string ThvlFax { get; set; }

        public string ThvlEMail { get; set; }

        public string ThvlAddress { get; set; }

        public string ThvlOstan { get; set; }

        public string ThvlShahrestan { get; set; }

        public string ThvlEcoCode { get; set; }

        public string ThvlMelliCode { get; set; }

    }
}