namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_Kala_App
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string KGruCode { get; set; }

        public string BarCode { get; set; }
        
        public double? Mjd { get; set; }

    }
}