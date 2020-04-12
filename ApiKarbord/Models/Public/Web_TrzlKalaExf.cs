namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_TrzIKalaExf
    {
        [Key]
        [StringLength(100)]
        public string KalaCode { get; set; }

        [StringLength(250)]
        public string KalaName { get; set; }

        [StringLength(50)]
        public string KalaFanniNo { get; set; }

        [StringLength(50)]
        public string InvCode { get; set; }

        [StringLength(250)]
        public string InvName { get; set; }

        [StringLength(50)]
        public string KalaUnitName1 { get; set; }

        [StringLength(50)]
        public string KalaUnitName2 { get; set; }

        [StringLength(50)]
        public string KalaUnitName3 { get; set; }

        [StringLength(50)]
        public string KGruCode { get; set; }

        [StringLength(50)]
        public string KGruName { get; set; }

        public byte? KalaDeghat1 { get; set; }

        public byte? KalaDeghat2 { get; set; }

        public byte? KalaDeghat3 { get; set; }

        public double? AAmount1 { get; set; }

        public double? AAmount2 { get; set; }

        public double? AAmount3 { get; set; }

        public double? VAmount1 { get; set; }

        public double? VAmount2 { get; set; }

        public double? VAmount3 { get; set; }

        public double? SAmount1 { get; set; }

        public double? SAmount2 { get; set; }

        public double? SAmount3 { get; set; }

        public double? MAmount1 { get; set; }

        public double? MAmount2 { get; set; }

        public double? MAmount3 { get; set; }

        public double? ATotalPrice { get; set; }

        public double? VTotalPrice { get; set; }

        public double? STotalPrice { get; set; }

        public double? MTotalPrice { get; set; }

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

        public int? Tag { get; set; }

    }
}
