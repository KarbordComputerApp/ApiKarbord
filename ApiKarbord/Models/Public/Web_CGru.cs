﻿namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_CGru
    {
        [Key]

        public string Code { get; set; }

        public long SortCode { get; set; }

        public string Name { get; set; }

        public long SortName { get; set; }

        public string Spec { get; set; }

        public int Level { get; set; }
       
        public short? Mode { get; set; }
    }
}