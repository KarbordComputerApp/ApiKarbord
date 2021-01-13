namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_DocAttach
    {

        public long? SerialNumber { get; set; }

        public int? BandNo { get; set; }

        [StringLength(250)]
        public string Comm { get; set; }

        [StringLength(100)]
        public string FName { get; set; }

    }
}