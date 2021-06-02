namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_Krdx
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SerialNumber { get; set; }

        public byte? BodyTag { get; set; }

        public long OrderNumber { get; set; }

        public int? InOut { get; set; }

        public long? DocNo { get; set; }


        public string DocDate { get; set; }


        public string Spec { get; set; }


        public string Status { get; set; }


        public string Eghdam { get; set; }


        public string Tanzim { get; set; }


        public string Taeed { get; set; }


        public string Tasvib { get; set; }


        public string ModeCode { get; set; }

        [Key]
        [Column(Order = 1)]

        public string ModeName { get; set; }



        public string ThvlCode { get; set; }

        [Key]
        [Column(Order = 3)]

        public string ThvlName { get; set; }


        public string IDocF01 { get; set; }


        public string IDocF02 { get; set; }


        public string IDocF03 { get; set; }


        public string IDocF04 { get; set; }


        public string IDocF05 { get; set; }


        public string IDocF06 { get; set; }


        public string IDocF07 { get; set; }


        public string IDocF08 { get; set; }


        public string IDocF09 { get; set; }


        public string IDocF10 { get; set; }


        public string IDocF11 { get; set; }


        public string IDocF12 { get; set; }


        public string IDocF13 { get; set; }


        public string IDocF14 { get; set; }


        public string IDocF15 { get; set; }


        public string IDocF16 { get; set; }


        public string IDocF17 { get; set; }


        public string IDocF18 { get; set; }


        public string IDocF19 { get; set; }


        public string IDocF20 { get; set; }

        public int? BandNo { get; set; }

        [Key]
        [Column(Order = 4)]

        public string MkzCode { get; set; }

        public string SortMkzCode { get; set; }

        [Key]
        [Column(Order = 5)]

        public string MkzName { get; set; }

        
        [Key]
        [Column(Order = 9)]

        public string OprCode { get; set; }

        [Key]
        [Column(Order = 10)]

        public string OprName { get; set; }

        [Key]
        [Column(Order = 11)]

        public string BandSpec { get; set; }


        public string DimX { get; set; }


        public string DimY { get; set; }


        public string DimZ { get; set; }

        public double? iAddMin1 { get; set; }

        public double? iAddMin2 { get; set; }

        public double? iAddMin3 { get; set; }


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

        [Key]
        [Column(Order = 12)]

        public string Comm { get; set; }

        public long? InDocNo { get; set; }

        public long? OutDocNo { get; set; }

        public double? VAmount1 { get; set; }

        public double? VAmount2 { get; set; }

        public double? VAmount3 { get; set; }

        public double? VTotalPrice { get; set; }

        public double? SAmount1 { get; set; }

        public double? SAmount2 { get; set; }

        public double? SAmount3 { get; set; }

        public double? STotalPrice { get; set; }

        public double? SumVAmount1 { get; set; }

        public double? SumVAmount2 { get; set; }

        public double? SumVAmount3 { get; set; }

        public double? SumVTotalPrice { get; set; }

        public double? SumSAmount1 { get; set; }

        public double? SumSAmount2 { get; set; }

        public double? SumSAmount3 { get; set; }

        public double? SumSTotalPrice { get; set; }

        public double? MAmount1 { get; set; }

        public double? MAmount2 { get; set; }

        public double? MAmount3 { get; set; }

        public double? MTotalPrice { get; set; }

        public double? VUnitPrice1 { get; set; }

        public double? VUnitPrice2 { get; set; }

        public double? VUnitPrice3 { get; set; }

        public double? SUnitPrice1 { get; set; }

        public double? SUnitPrice2 { get; set; }

        public double? SUnitPrice3 { get; set; }

        public double? MUnitPrice1 { get; set; }

        public double? MUnitPrice2 { get; set; }

        public double? MUnitPrice3 { get; set; }

        public byte? KalaDeghatM1 { get; set; }

        public byte? KalaDeghatM2 { get; set; }

        public byte? KalaDeghatM3 { get; set; }

        public byte? KalaDeghatR1 { get; set; }

        public byte? KalaDeghatR2 { get; set; }

        public byte? KalaDeghatR3 { get; set; }

    }
}