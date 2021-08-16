namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_KGru
    {
        [Key]
        public string Code { get; set; }

        public long? SortCode { get; set; }

        public string Name { get; set; }

        public long? SortName { get; set; }

        public string Spec { get; set; }

        public int Level { get; set; }
        
        public string Eghdam { get; set; }

        public DateTime? EghdamDate { get; set; }

        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}