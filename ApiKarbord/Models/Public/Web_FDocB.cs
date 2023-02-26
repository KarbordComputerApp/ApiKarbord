namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_FDocB
    {
        [Key]
        public long SerialNumber { get; set; }

        public int BandNo { get; set; }


        public string KalaCode { get; set; }


        public string KalaName { get; set; }

        public short? MainUnit { get; set; }

        public string MainUnitName { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double? UnitPrice { get; set; }

        public double? TotalPrice { get; set; }

        public double? Discount { get; set; }

        public string Comm { get; set; }

        public string BandSpec { get; set; }

        public bool UP_Flag { get; set; }

        public byte? KalaDeghatM1 { get; set; }

        public byte? KalaDeghatM2 { get; set; }

        public byte? KalaDeghatM3 { get; set; }

        public byte? KalaDeghatR1 { get; set; }

        public byte? KalaDeghatR2 { get; set; }

        public byte? KalaDeghatR3 { get; set; }

        public byte? DeghatR { get; set; }


        public long? LFctSerialNumber { get; set; }

        public long? InvSerialNumber { get; set; }

        public long? LinkNumber { get; set; }

        public Int16? LinkYear { get; set; }

        public string LinkProg { get; set; }

        public double? ArzValue { get; set; }

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