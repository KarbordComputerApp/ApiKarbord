namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class AFI_FDocBi
    {

        [Key]
        public long SerialNumber { get; set; }

        public int? BandNo { get; set; }

        public string KalaCode { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double? UnitPrice { get; set; }

        public double? TotalPrice { get; set; }

        public double? Discount { get; set; }

        public int? MainUnit { get; set; }

        public string Comm { get; set; }

        public string BandSpec { get; set; }

        public bool Up_Flag { get; set; }

        public string ModeCode { get; set; }

        public string flagLog { get; set; }

        public string OprCode { get; set; }

        public string MkzCode { get; set; }

        public string InvCode { get; set; }

        public long? LFctSerialNumber { get; set; }

        public long? InvSerialNumber { get; set; }

        public long? LinkNumber { get; set; }

        public int? LinkYear { get; set; }

        public string LinkProg { get; set; }

        public int? LinkBandNo { get; set; }

        public string ArzCode { get; set; }

        public double? ArzRate { get; set; }

        public double? ArzValue { get; set; }

        public byte? MjdControl { get; set; }

        public string KalaFileNo { get; set; }

        public string KalaState { get; set; }

        public string KalaExf1 { get; set; }

        public string KalaExf2 { get; set; }

        public string KalaExf3 { get; set; }

        public string KalaExf4 { get; set; }

        public string KalaExf5 { get; set; }

        public string KalaExf6 { get; set; }

        public string KalaExf7 { get; set; }

        public string KalaExf8 { get; set; }

        public string KalaExf9 { get; set; }

        public string KalaExf10 { get; set; }

        public string KalaExf11 { get; set; }

        public string KalaExf12 { get; set; }

        public string KalaExf13 { get; set; }

        public string KalaExf14 { get; set; }

        public string KalaExf15 { get; set; }

    }
}