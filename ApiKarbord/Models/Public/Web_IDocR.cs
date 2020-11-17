namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class Web_IDocR
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string Tag { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SerialNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandNo { get; set; }

        [StringLength(100)]
        public string KalaCode { get; set; }

        [StringLength(50)]
        public string MkzCode { get; set; }

        [StringLength(50)]
        public string OprCode { get; set; }

        public Int16? MainUnit { get; set; }

        [StringLength(250)]
        public string BandSpec { get; set; }

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

        public double? UnitPrice { get; set; }

        public long? DocNo { get; set; }

        [StringLength(10)]
        public string DocDate { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 3)]
        public string Tanzim { get; set; }

        [StringLength(10)]
        public string Tasvib { get; set; }

        [StringLength(10)]
        public string Taeed { get; set; }

        [StringLength(10)]
        public string InvCode { get; set; }

        //[StringLength(10)]
       // public int ModeCode { get; set; }

        [StringLength(50)]
        public string ThvlCode { get; set; }

        public int? InOut { get; set; }

        /* [StringLength(250)]
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
         public string F20 { get; set; } */

        [StringLength(250)]
        public string KalaName { get; set; }

        [Key]
        [Column(Order = 4)]
        public string KalaFanniNo { get; set; }

        public byte? KalaDeghatM1 { get; set; }

        public byte? KalaDeghatM2 { get; set; }

        public byte? KalaDeghatM3 { get; set; }

        public byte? KalaDeghatR1 { get; set; }

        public byte? KalaDeghatR2 { get; set; }

        public byte? KalaDeghatR3 { get; set; }

        public double? KalaZarib1 { get; set; }

        public double? KalaZarib2 { get; set; }

        public double? KalaZarib3 { get; set; }

        [StringLength(50)]
        public string KalaUnitName1 { get; set; }

        [StringLength(50)]
        public string KalaUnitName2 { get; set; }

        [StringLength(50)]
        public string KalaUnitName3 { get; set; }


        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string ThvlName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string TGruCode { get; set; }

        [Key]
        [Column(Order = 7)]
        public string TGruName { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(250)]
        public string InvName { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(250)]
        public string ModeName { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(250)]
        public string MkzName { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(100)]
        public string OprName { get; set; }

        [StringLength(250)]
        public string Comm { get; set; }

        public double? VAmount1 { get; set; }

        public double? VAmount2 { get; set; }

        public double? VAmount3 { get; set; }

        public double? SAmount1 { get; set; }

        public double? SAmount2 { get; set; }

        public double? SAmount3 { get; set; }

        public double? TotalPrice { get; set; }

        public double? SortDocNo { get; set; }

        [StringLength(50)]
        public string MainUnitName { get; set; }

        public int? DeghatR { get; set; }

        [Key]
        [Column(Order = 12)]
        public double Amount1 { get; set; }

        [Key]
        [Column(Order = 13)]
        public double Amount2 { get; set; }

        [Key]
        [Column(Order = 14)]
        public double Amount3 { get; set; }

        [Key]
        [Column(Order = 15)]
        public double FinalPrice { get; set; }
    }
}
