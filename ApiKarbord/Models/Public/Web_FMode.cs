namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_FMode
    {

        [Key]
        [StringLength(30)]
        public string Code { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int? InOut { get; set; }

        [StringLength(1)]
        public string Spec { get; set; }

    }
}