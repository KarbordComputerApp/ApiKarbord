﻿namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_ZAcc
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public long? SortName { get; set; }

        public string Spec { get; set; }

        public string ZGruCode { get; set; }

        public string ZGruName { get; set; }

    }
}