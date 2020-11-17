namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class TestDocB
    {
        [Key]
        public int id { get; set; }
        public byte? SvTest { get; set; }
        public string SvTestName { get; set; }
        public int? BandNo { get; set; }
        public string AccCode { get; set; }
    }
}