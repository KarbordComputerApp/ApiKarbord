namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_FDocR
    {
        [Key]
        [Column(Order = 0)]
        public long SerialNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        public int BandNo { get; set; }


        public string KalaCode { get; set; }

        public string SortMkzCode { get; set; }



        public string MkzCode { get; set; }


        public string OprCode { get; set; }


        public string InvCode { get; set; }

        public short? MainUnit { get; set; }

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


        public string BandSpec { get; set; }


        public string Comm { get; set; }

        public double? Discount { get; set; }

        public double? UnitPrice { get; set; }

        public double? TotalPrice { get; set; }


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


        public double? SortDocNo { get; set; }


        public string DocNo { get; set; }


        public string DocDate { get; set; }


        public string Spec { get; set; }


        public string Status { get; set; }


        public string Tanzim { get; set; }


        public string Taeed { get; set; }


        public string Eghdam { get; set; }


        public string Tasvib { get; set; }


        public string ModeCode { get; set; }


        public string ModeName { get; set; }


        public string CustCode { get; set; }


        public string KalaName { get; set; }

        public double? KalaZarib1 { get; set; }

        public double? KalaZarib2 { get; set; }

        public double? KalaZarib3 { get; set; }


        public string KalaUnitName1 { get; set; }


        public string KalaUnitName2 { get; set; }


        public string KalaUnitName3 { get; set; }


        public string KalaFanniNo { get; set; }

        public byte? KalaDeghatM1 { get; set; }

        public byte? KalaDeghatM2 { get; set; }

        public byte? KalaDeghatM3 { get; set; }

        public byte? KalaDeghatR1 { get; set; }

        public byte? KalaDeghatR2 { get; set; }

        public byte? KalaDeghatR3 { get; set; }


        public string KGruCode { get; set; }


        public string CustName { get; set; }


        public string MkzName { get; set; }


        public string OprName { get; set; }


        public string InvName { get; set; }


        public string MainUnitName { get; set; }

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

    }
}