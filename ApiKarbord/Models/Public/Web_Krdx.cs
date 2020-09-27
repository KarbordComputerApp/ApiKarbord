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

        public long OrderNumber { get; set; }

        public int? InOut { get; set; }

        public long? DocNo { get; set; }

        [StringLength(10)]
        public string DocDate { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(10)]
        public string Eghdam { get; set; }

        [StringLength(10)]
        public string Tanzim { get; set; }

        [StringLength(10)]
        public string Taeed { get; set; }

        [StringLength(10)]
        public string Tasvib { get; set; }

        [StringLength(30)]
        public string ModeCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string ModeName { get; set; }


        [StringLength(50)]
        public string ThvlCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(250)]
        public string ThvlName { get; set; }

        [StringLength(250)]
        public string F01 { get; set; }

        [StringLength(250)]
        public string F02 { get; set; }

        [StringLength(250)]
        public string F03 { get; set; }

        [StringLength(250)]
        public string F04 { get; set; }

        [StringLength(250)]
        public string F05 { get; set; }

        [StringLength(250)]
        public string F06 { get; set; }

        [StringLength(250)]
        public string F07 { get; set; }

        [StringLength(250)]
        public string F08 { get; set; }

        [StringLength(250)]
        public string F09 { get; set; }

        [StringLength(250)]
        public string F10 { get; set; }

        [StringLength(250)]
        public string F11 { get; set; }

        [StringLength(250)]
        public string F12 { get; set; }

        [StringLength(250)]
        public string F13 { get; set; }

        [StringLength(250)]
        public string F14 { get; set; }

        [StringLength(250)]
        public string F15 { get; set; }

        [StringLength(250)]
        public string F16 { get; set; }

        [StringLength(250)]
        public string F17 { get; set; }

        [StringLength(250)]
        public string F18 { get; set; }

        [StringLength(250)]
        public string F19 { get; set; }

        [StringLength(250)]
        public string F20 { get; set; }

        public int? BandNo { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string MkzCode { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(250)]
        public string MkzName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MkzCode1 { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MkzCode2 { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MkzCode3 { get; set; }

        public long? MkzCode4 { get; set; }

        public long? MkzCode5 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string OprCode { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string OprName { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(250)]
        public string BandSpec { get; set; }

        [StringLength(10)]
        public string DimX { get; set; }

        [StringLength(10)]
        public string DimY { get; set; }

        [StringLength(10)]
        public string DimZ { get; set; }

        public double? iAddMin1 { get; set; }

        public double? iAddMin2 { get; set; }

        public double? iAddMin3 { get; set; }

        [StringLength(50)]
        public string KalaFileNo { get; set; }

        [StringLength(50)]
        public string KalaState { get; set; }

        [StringLength(50)]
        public string KalaExf1 { get; set; }

        [StringLength(50)]
        public string KalaExf2 { get; set; }

        [StringLength(50)]
        public string KalaExf3 { get; set; }

        [StringLength(50)]
        public string KalaExf4 { get; set; }

        [StringLength(50)]
        public string KalaExf5 { get; set; }

        [StringLength(50)]
        public string KalaExf6 { get; set; }

        [StringLength(50)]
        public string KalaExf7 { get; set; }

        [StringLength(50)]
        public string KalaExf8 { get; set; }

        [StringLength(50)]
        public string KalaExf9 { get; set; }

        [StringLength(50)]
        public string KalaExf10 { get; set; }

        [StringLength(50)]
        public string KalaExf11 { get; set; }

        [StringLength(50)]
        public string KalaExf12 { get; set; }

        [StringLength(50)]
        public string KalaExf13 { get; set; }

        [StringLength(50)]
        public string KalaExf14 { get; set; }

        [StringLength(50)]
        public string KalaExf15 { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(4000)]
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

        public int? DeghatM1 { get; set; }

        public int? DeghatM2 { get; set; }

        public int? DeghatM3 { get; set; }

        public int? DeghatR1 { get; set; }

        public int? DeghatR2 { get; set; }

        public int? DeghatR3 { get; set; }

    }
}