namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_IDocP
    {
        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandNo { get; set; }

        [StringLength(4000)]
        public string BandSpec { get; set; }

        [StringLength(4000)]
        public string Comm { get; set; }

        [StringLength(100)]
        public string KalaCode { get; set; }

        public short? MainUnit { get; set; }

        [StringLength(50)]
        public string MkzCode { get; set; }

        [StringLength(50)]
        public string OprCode { get; set; }

        [StringLength(50)]
        public string PrdCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SerialNumber { get; set; }

        public double? TotalPrice { get; set; }

        public double? UnitPrice { get; set; }

        public bool? UP_Flag { get; set; }

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

        [StringLength(50)]
        public string MainUnitName { get; set; }

        public int? DeghatR { get; set; }

        [StringLength(10)]
        public string DocNo { get; set; }

        [StringLength(10)]
        public string DocDate { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(50)]
        public string ThvlCode { get; set; }

        [StringLength(100)]
        public string ThvlName { get; set; }

        [StringLength(50)]
        public string InvCode { get; set; }

        [StringLength(250)]
        public string InvName { get; set; }

        [StringLength(30)]
        public string ModeCode { get; set; }

        [StringLength(250)]
        public string ModeName { get; set; }

        [StringLength(4000)]
        public string Footer { get; set; }
    }
}
