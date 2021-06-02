namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_ExtraFields
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandNo { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ModeCode { get; set; }

        public string Name { get; set; }

        public short? Type { get; set; }
    }
}
