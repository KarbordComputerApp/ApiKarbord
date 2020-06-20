namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_CheckStatus
    {
        [Key]
        [Column(Order = 0)]
        public int PDMode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Name { get; set; }

    }
}