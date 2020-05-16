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
        public long Serialnumber { get; set; }

        [Key]
        [Column(Order = 1)]
        public int BandNo { get; set; }

        [StringLength(100)]
        public string KalaCode { get; set; }

        [StringLength(50)]
        public string MkzCode { get; set; }

        [StringLength(50)]
        public string OprCode { get; set; }

        public short? MainUnit { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        [StringLength(250)]
        public string BandSpec { get; set; }

        [Column(Order = 2)]
        [StringLength(4000)]
        public string Comm { get; set; }

        public double? UnitPrice { get; set; }

        public double? TotalPrice { get; set; }

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

        [StringLength(10)]
        public string DocNo { get; set; }

        [StringLength(10)]
        public string DocDate { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(10)]
        public string Tanzim { get; set; }

        [StringLength(10)]
        public string Taeed { get; set; }

        [StringLength(10)]
        public string Eghdam { get; set; }

        [StringLength(10)]
        public string Tasvib { get; set; }

        [StringLength(30)]
        public string ModeCode { get; set; }

        [StringLength(50)]
        public string CustCode { get; set; }

        [StringLength(250)]
        public string KalaName { get; set; }

        public double? KalaZarib1 { get; set; }

        public double? KalaZarib2 { get; set; }

        public double? KalaZarib3 { get; set; }

        [StringLength(50)]
        public string KalaUnitName1 { get; set; }

        [StringLength(50)]
        public string KalaUnitName2 { get; set; }

        [StringLength(50)]
        public string KalaUnitName3 { get; set; }

        [StringLength(50)]
        public string KalaFanniNo { get; set; }

        public int? DeghatM1 { get; set; }

        public int? DeghatM2 { get; set; }

        public int? DeghatM3 { get; set; }

        public int? DeghatR1 { get; set; }

        public int? DeghatR2 { get; set; }

        public int? DeghatR3 { get; set; }

        [StringLength(50)]
        public string KGruCode { get; set; }

        [StringLength(250)]
        public string CustName { get; set; }

        [Column(Order = 3)]
        [StringLength(250)]
        public string MkzName { get; set; }

        [Column(Order = 4)]
        [StringLength(100)]
        public string OprName { get; set; }
    }
}