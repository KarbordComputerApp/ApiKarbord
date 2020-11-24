namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_Param
    {

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string Node { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string Key { get; set; }

        [StringLength(250)]
        public string Param { get; set; }
    }
}