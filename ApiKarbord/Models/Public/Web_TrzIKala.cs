﻿namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_TrzIKala
    {
        [Key]
        public string KalaCode { get; set; }

        public long? SortKalaCode { get; set; }

        public string KalaName { get; set; }

        public long? SortKalaName { get; set; }

        public string KalaFanniNo { get; set; }

        public string InvCode { get; set; }

        public string InvName { get; set; }

        public long? SortInvCode { get; set; }

        public long? SortInvName { get; set; }

        public string KalaUnitName1 { get; set; }

        public string KalaUnitName2 { get; set; }

        public string KalaUnitName3 { get; set; }

        public string KGruCode { get; set; }

        public string KGruName { get; set; }

        public byte? KalaDeghatM1 { get; set; }

        public byte? KalaDeghatM2 { get; set; }

        public byte? KalaDeghatM3 { get; set; }

        public byte? KalaDeghatR1 { get; set; }

        public byte? KalaDeghatR2 { get; set; }

        public byte? KalaDeghatR3 { get; set; }


        public double? AAmount1 { get; set; }

        public double? AAmount2 { get; set; }

        public double? AAmount3 { get; set; }

        public double? VAmount1 { get; set; }

        public double? VAmount2 { get; set; }

        public double? VAmount3 { get; set; }

        public double? SAmount1 { get; set; }

        public double? SAmount2 { get; set; }

        public double? SAmount3 { get; set; }

        public double? MAmount1 { get; set; }

        public double? MAmount2 { get; set; }

        public double? MAmount3 { get; set; }

        public double? ATotalPrice { get; set; }

        public double? VTotalPrice { get; set; }

        public double? STotalPrice { get; set; }

        public double? MTotalPrice { get; set; }
    }
}
