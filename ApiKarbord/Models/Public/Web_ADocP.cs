namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_ADocP
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandNo { get; set; }

        public long? bSerialNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string AccFullCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string TrafFullCode { get; set; }

        [StringLength(50)]
        public string MkzCode { get; set; }

        [StringLength(50)]
        public string OprCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(250)]
        public string BandSpec { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(4000)]
        public string Comm { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(30)]
        public string CheckNo { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string CheckDate { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string Bank { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string Shobe { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string Jari { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(20)]
        public string BaratNo { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(4000)]
        public string CheckComm { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CheckMode { get; set; }

        public int? CheckRadif { get; set; }

        [StringLength(10)]
        public string CheckVosoolDate { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        [Key]
        [Column(Order = 13)]
        public double Amount { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(50)]
        public string ArzCode { get; set; }

        [Key]
        [Column(Order = 15)]
        public double ArzValue { get; set; }

        [Key]
        [Column(Order = 16)]
        public double ArzRate { get; set; }

        [StringLength(4000)]
        public string CheckStatus { get; set; }

        public long? SerialNumber { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(100)]
        public string AccFullName { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode1 { get; set; }

        [Key]
        [Column(Order = 19)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode2 { get; set; }

        [Key]
        [Column(Order = 20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode3 { get; set; }

        [Key]
        [Column(Order = 21)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode4 { get; set; }

        [Key]
        [Column(Order = 22)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode5 { get; set; }

        [Key]
        [Column(Order = 23)]
        [StringLength(15)]
        public string AccZCode { get; set; }

        [Key]
        [Column(Order = 24)]
        [StringLength(250)]
        public string ZGru { get; set; }

        public short? Mkz { get; set; }

        public short? Opr { get; set; }

        public short? Arzi { get; set; }

        public short? PDMode { get; set; }

        [Key]
        [Column(Order = 25)]
        [StringLength(50)]
        public string ArzName { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(100)]
        public string TrafFullName { get; set; }

        [Key]
        [Column(Order = 27)]
        [StringLength(15)]
        public string TrafZCode { get; set; }

        [Key]
        [Column(Order = 28)]
        [StringLength(250)]
        public string MkzName { get; set; }

        [Key]
        [Column(Order = 29)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MkzCode1 { get; set; }

        [Key]
        [Column(Order = 30)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MkzCode2 { get; set; }

        [Key]
        [Column(Order = 31)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MkzCode3 { get; set; }

        public long? MkzCode4 { get; set; }

        public long? MkzCode5 { get; set; }

        [Key]
        [Column(Order = 32)]
        [StringLength(100)]
        public string OprName { get; set; }

        [Key]
        [Column(Order = 33)]
        [StringLength(100)]
        public string AccZName { get; set; }

        [Key]
        [Column(Order = 34)]
        [StringLength(100)]
        public string TrafZName { get; set; }

        [StringLength(50)]
        public string AccCode { get; set; }

        [StringLength(50)]
        public string TrafCode { get; set; }

        [Key]
        [Column(Order = 35)]
        [StringLength(100)]
        public string AccName { get; set; }

        [Key]
        [Column(Order = 36)]
        [StringLength(100)]
        public string TrafName { get; set; }

        public long? DocNo { get; set; }

        [StringLength(10)]
        public string DocDate { get; set; }

        [StringLength(250)]
        public string Spec { get; set; }
    }
}