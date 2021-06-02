namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_CheckList
    {
        [Key]
        public long? Row { get; set; }

        public double? Value { get; set; }


        public string CheckNo { get; set; }


        public string CheckDate { get; set; }


        public string Bank { get; set; }


        public string Shobe { get; set; }


        public string Jari { get; set; }


        public string BaratNo { get; set; }

        public int? CheckStatus { get; set; }

        public int? CheckRadif { get; set; }


        public string CheckComm { get; set; }


        public string CheckVosoolDate { get; set; }


        public string CheckStatusSt { get; set; }


        public string TrafFullCode { get; set; }

        [Column(Order = 0)]

        public string TrafFullName { get; set; }

        [Column(Order = 1)]

        public string TrafZCode { get; set; }

        [Column(Order = 2)]

        public string TrafZName { get; set; }

        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PDMode { get; set; }


        public string TrafCode { get; set; }

        [Column(Order = 4)]

        public string TrafName { get; set; }
    }
}