namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Web_ErjDocB_Last")]
    public class Web_ErjDocB_Last
    {
        [StringLength(100)]
        public string CustName { get; set; }

        [StringLength(100)]
        public string KhdtName { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SerialNumber { get; set; }

        [StringLength(10)]
        public string MhltDate { get; set; }

        [Column(TypeName = "ntext")]
        public string EghdamComm { get; set; }

        [Column(TypeName = "ntext")]
        public string SpecialComm { get; set; }

        [Column(TypeName = "ntext")]
        public string FinalComm { get; set; }

        [Column(TypeName = "ntext")]
        public string DocDesc { get; set; }

        public int? DocBStep { get; set; }

        [StringLength(5)]
        public string RjRadif { get; set; }

        public int? BandNo { get; set; }

        public int? DocBMode { get; set; }

        [Column(TypeName = "ntext")]
        public string RjComm { get; set; }

        [StringLength(50)]
        public string RjDate { get; set; }

        [StringLength(50)]
        public string RjStatus { get; set; }

        [StringLength(50)]
        public string RjEndDate { get; set; }

        [StringLength(10)]
        public string RjMhltDate { get; set; }

        public DateTime? RjUpdateDate { get; set; }

        [StringLength(20)]
        public string RjUpdateUser { get; set; }

        public int? ErjaCount { get; set; }

        public double? RjTime { get; set; }

        [StringLength(38)]
        public string FromUserCode { get; set; }

        [StringLength(68)]
        public string FromUserName { get; set; }

        [StringLength(32)]
        public string ToUserCode { get; set; }

        [StringLength(62)]
        public string ToUserName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string RjReadSt { get; set; }
    }
}