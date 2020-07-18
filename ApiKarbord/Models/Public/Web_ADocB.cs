namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_ADocB
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SerialNumber { get; set; }

        [StringLength(50)]
        public string AccCode { get; set; }

        [StringLength(50)]
        public string MkzCode { get; set; }

        [StringLength(50)]
        public string OprCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4000)]
        public string Comm { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(30)]
        public string CheckNo { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(10)]
        public string CheckDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string Bank { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string Shobe { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string Jari { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string TrafCode { get; set; }

        public double? Bede { get; set; }

        public double? Best { get; set; }

        [Key]
        [Column(Order = 9)]
        public double Amount { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(50)]
        public string ArzCode { get; set; }

        [Key]
        [Column(Order = 11)]
        public double ArzValue { get; set; }

        [Key]
        [Column(Order = 12)]
        public double ArzRate { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(100)]
        public string AccName { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode1 { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode2 { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode3 { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode4 { get; set; }

        [Key]
        [Column(Order = 18)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccCode5 { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(50)]
        public string ArzName { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(100)]
        public string TrafName { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(250)]
        public string MkzName { get; set; }

        [Key]
        [Column(Order = 22)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MkzCode1 { get; set; }

        [Key]
        [Column(Order = 23)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MkzCode2 { get; set; }

        [Key]
        [Column(Order = 24)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MkzCode3 { get; set; }

        public long? MkzCode4 { get; set; }

        public long? MkzCode5 { get; set; }

        [Key]
        [Column(Order = 25)]
        [StringLength(100)]
        public string OprName { get; set; }
    }
}