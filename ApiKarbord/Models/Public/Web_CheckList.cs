namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_CheckList
    {
        
        public double? Value { get; set; }

        [Key]
        [StringLength(20)]
        public string CheckNo { get; set; }

        [StringLength(10)]
        public string CheckDate { get; set; }

        [Key]
        [StringLength(20)]
        public string Bank { get; set; }

        [Key]
        [StringLength(20)]
        public string Shobe { get; set; }

        [StringLength(20)]
        public string Jari { get; set; }

        [StringLength(20)]
        public string BaratNo { get; set; }

        public int? CheckStatus { get; set; }

        public int? CheckRadif { get; set; }

        [StringLength(250)]
        public string CheckComm { get; set; }

        [StringLength(10)]
        public string CheckVosoolDate { get; set; }

        [StringLength(20)]
        public string CheckStatusSt { get; set; }

        [StringLength(50)]
        public string TrafFullCode { get; set; }


        [Column(Order = 0)]
        [StringLength(100)]
        public string TrafFullName { get; set; }


        [Column(Order = 1)]
        [StringLength(15)]
        public string TrafZCode { get; set; }


        [Column(Order = 2)]
        [StringLength(100)]
        public string TrafZName { get; set; }


        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PDMode { get; set; }

        [StringLength(50)]
        public string TrafCode { get; set; }


        [Column(Order = 4)]
        [StringLength(100)]
        public string TrafName { get; set; }
    }
}