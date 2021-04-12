﻿namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class AFI_FDocHi
    {
        [Key]
        public long SerialNumber { get; set; }
        public byte? DocNoMode { get; set; }

        public byte? InsertMode { get; set; }

        public string ModeCode { get; set; }

        public string DocNo { get; set; }

        public int? StartNo { get; set; }

        public int? EndNo { get; set; }

        public byte? BranchCode { get; set; }

        public string UserCode { get; set; }

        public string DocDate { get; set; }

        public string Spec { get; set; }

        public string Tanzim { get; set; }

        public string TahieShode { get; set; }

        public string CustCode { get; set; }

        public string VstrCode { get; set; }

        public double? VstrPer { get; set; }

        public string PakhshCode { get; set; }

        public int? KalaPriceCode { get; set; }

        public string AddMinSpec1 { get; set; }

        public string AddMinSpec2 { get; set; }

        public string AddMinSpec3 { get; set; }

        public string AddMinSpec4 { get; set; }

        public string AddMinSpec5 { get; set; }

        public string AddMinSpec6 { get; set; }

        public string AddMinSpec7 { get; set; }

        public string AddMinSpec8 { get; set; }

        public string AddMinSpec9 { get; set; }

        public string AddMinSpec10 { get; set; }

        public double? AddMinPrice1 { get; set; }

        public double? AddMinPrice2 { get; set; }

        public double? AddMinPrice3 { get; set; }

        public double? AddMinPrice4 { get; set; }

        public double? AddMinPrice5 { get; set; }

        public double? AddMinPrice6 { get; set; }

        public double? AddMinPrice7 { get; set; }

        public double? AddMinPrice8 { get; set; }

        public double? AddMinPrice9 { get; set; }

        public double? AddMinPrice10 { get; set; }

        public string InvCode { get; set; }
        
        public int? DocNo_Out { get; set; }

        public string Status  { get; set; }

        public byte? PaymentType { get; set; }

        public string Footer { get; set; }
        public string Eghdam { get; set; }
        public string EghdamDate { get; set; }

        public int deghat { get; set; }

        public string Taeed { get; set; }

        [StringLength(250)]
        public string F01 { get; set; }

        [StringLength(250)]
        public string F02 { get; set; }

        [StringLength(250)]
        public string F03 { get; set; }

        [StringLength(250)]
        public string F04 { get; set; }

        [StringLength(250)]
        public string F05 { get; set; }

        [StringLength(250)]
        public string F06 { get; set; }

        [StringLength(250)]
        public string F07 { get; set; }

        [StringLength(250)]
        public string F08 { get; set; }

        [StringLength(250)]
        public string F09 { get; set; }

        [StringLength(250)]
        public string F10 { get; set; }

        [StringLength(250)]
        public string F11 { get; set; }

        [StringLength(250)]
        public string F12 { get; set; }

        [StringLength(250)]
        public string F13 { get; set; }

        [StringLength(250)]
        public string F14 { get; set; }

        [StringLength(250)]
        public string F15 { get; set; }

        [StringLength(250)]
        public string F16 { get; set; }

        [StringLength(250)]
        public string F17 { get; set; }

        [StringLength(250)]
        public string F18 { get; set; }

        [StringLength(250)]
        public string F19 { get; set; }

        [StringLength(250)]
        public string F20 { get; set; }

        public string Tasvib { get; set; }

        public string flagLog { get; set; }

    }
}