namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_Kala_Info
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string KGruCode { get; set; }

        public string KGruName { get; set; }

        public double? Mjd { get; set; }

        [Column(TypeName = "image")]
        public byte[] KalaImage { get; set; }

        public string Comm { get; set; }

    }
}