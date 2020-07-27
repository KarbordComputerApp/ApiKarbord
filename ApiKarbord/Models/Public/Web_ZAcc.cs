namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_ZAcc
    {
       
        public int Mode { get; set; }

        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Spec { get; set; }

        public string ZGruCode { get; set; }

    }
}