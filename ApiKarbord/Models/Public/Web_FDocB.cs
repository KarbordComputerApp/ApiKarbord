namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_FDocB
    {
        [Key]
        public long SerialNumber { get; set; }

        public int BandNo { get; set; }


        public string KalaCode { get; set; }


        public string KalaName { get; set; }

        public short? MainUnit { get; set; }

        public string MainUnitName { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double? UnitPrice { get; set; }

        public double? TotalPrice { get; set; }

        public double? Discount { get; set; }

        public string Comm { get; set; }

        public bool UP_Flag { get; set; }

        public byte? KalaDeghatM1 { get; set; }

        public byte? KalaDeghatM2 { get; set; }

        public byte? KalaDeghatM3 { get; set; }

        public byte? KalaDeghatR1 { get; set; }

        public byte? KalaDeghatR2 { get; set; }

        public byte? KalaDeghatR3 { get; set; }

        public byte? DeghatR { get; set; }

        // public int Deghat1 { get; set; }

        // public int Deghat2 { get; set; }

        //    public int Deghat3 { get; set; }

        /* SELECT
       [SerialNumber]
      ,[BandNo]
      ,[KalaCode]
	  ,[KalaName]
      ,[Amount1]
      ,[Amount2]
      ,[Amount3]
      ,[MainUnit]
      ,[UnitPrice]
      ,[TotalPrice]
      ,[Discount]
	  ,[Comm]
        FROM[ACE_AFI1011397].[dbo].[Web_FDocB]*/


    }
}