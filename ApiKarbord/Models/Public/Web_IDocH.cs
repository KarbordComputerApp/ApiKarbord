namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_IDocH
    {
        [Key]

        public long SerialNumber { get; set; }

        public int? InOut { get; set; }

        public double? SortDocNo { get; set; }


        public string ModeName { get; set; }


        public string ThvlCode { get; set; }

        public string ModeCode { get; set; }


        public string Footer { get; set; }


        public string DocNo { get; set; }



        public string DocDate { get; set; }



        public string Spec { get; set; }


        public string Status { get; set; }


        public string Tanzim { get; set; }


        public string Taeed { get; set; }


        public string TahieShode { get; set; }


        public byte PaymentType { get; set; }


        public string F01 { get; set; }



        public string F02 { get; set; }



        public string F03 { get; set; }


        public string F04 { get; set; }



        public string F05 { get; set; }


        public string F06 { get; set; }



        public string F07 { get; set; }


        public string F08 { get; set; }



        public string F09 { get; set; }


        public string F10 { get; set; }



        public string F11 { get; set; }


        public string F12 { get; set; }


        public string F13 { get; set; }


        public string F14 { get; set; }



        public string F15 { get; set; }


        public string F16 { get; set; }



        public string F17 { get; set; }


        public string F18 { get; set; }



        public string F19 { get; set; }


        public string F20 { get; set; }



        public string InvCode { get; set; }

        public int KalaPriceCode { get; set; }


        public string UpdateUser { get; set; }


        public DateTime UpdateDate { get; set; }


        public short Branch { get; set; }


        public string Eghdam { get; set; }

        public DateTime EghdamDate { get; set; }


        public short EghdamBranch { get; set; }


        public string ThvlName { get; set; }


        public string TGruCode { get; set; }


        public string TGruName { get; set; }


        public string InvName { get; set; }

        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double? TotalPrice { get; set; }

        public double? FinalPrice { get; set; }

        public string OprCode { get; set; }

        public string MkzCode { get; set; }

        public string MkzName { get; set; }

        public string OprName { get; set; }

        public string ArzCode { get; set; }

        public string ArzName { get; set; }

        public double? ArzRate { get; set; }


        /*[Key]
        public long SerialNumber { get; set; } 

        public string DocNo { get; set; }



        public string DocDate { get; set; }



       // public long? RSerialNumber { get; set; }


        public string Spec { get; set; }

        public int? ModeCode { get; set; }


        public string Status { get; set; }


        public string Tanzim { get; set; }


        public string Taeed { get; set; }


        public string TahieShode { get; set; }


        public string Footer { get; set; }

        public byte? PaymentType { get; set; }

       // 50)]
       // public string VstrCode { get; set; }

      //  public double? VstrPer { get; set; }


        public string ThvlCode { get; set; }


        public string PakhshCode { get; set; }


        public string hCustCity { get; set; }


        public string hCustRegion { get; set; }


        public string hCustStreet { get; set; }


        public string hCustAlley { get; set; }


        public string hCustPlack { get; set; }


        public string hCustZipCode { get; set; }


        public string hCustTel { get; set; }


        public string hCustMobile { get; set; }


        public string F01 { get; set; }


        public string F02 { get; set; }


        public string F03 { get; set; }


        public string F04 { get; set; }


        public string F05 { get; set; }


        public string F06 { get; set; }


        public string F07 { get; set; }


        public string F08 { get; set; }


        public string F09 { get; set; }


        public string F10 { get; set; }


        public string F11 { get; set; }


        public string F12 { get; set; }


        public string F13 { get; set; }


        public string F14 { get; set; }


        public string F15 { get; set; }


        public string F16 { get; set; }


        public string F17 { get; set; }


        public string F18 { get; set; }


        public string F19 { get; set; }


        public string F20 { get; set; }


        public string InvCode { get; set; }

        public long? OrderNumber { get; set; }

        //public long? OMoveSerialNumber { get; set; }


        public string AccCode { get; set; }

        public int? KalaPriceCode { get; set; }

       // public double? PishDaryaft { get; set; }

        //10)]
        //public string TasviyeDate { get; set; }


        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public short? Branch { get; set; }

       // public bool? RelatedGroupActive { get; set; }

        //public int? RelatedGroup_FromGroup { get; set; }

       // public long? RelatedGroup_FromSerialNumber { get; set; }
      
       // 20)]
       // public string PrintDate { get; set; }

     //   public int? PrintCount { get; set; }

     //   10)]
       // public string InvRegDocDate { get; set; }

      //  public short? RYear { get; set; }


        public string Eghdam { get; set; }

        public DateTime? EghdamDate { get; set; }

        public short? EghdamBranch { get; set; }

       // public byte? LegalRoozNoMode { get; set; }

       // public int? LegalRoozNo { get; set; }

      //  10)]
      //  public string ValidDate { get; set; }

        public long? LinkNumber { get; set; }


        public string thvlname { get; set; }

        //50)]
        //public string CGruCode { get; set; }


        public double? SortDocNo { get; set; }


        public double? Amount1 { get; set; }

        public double? Amount2 { get; set; }

        public double? Amount3 { get; set; }

        public double? TotalPrice { get; set; }

     //   public double? Discount { get; set; }

        public double? FinalPrice { get; set; }

      //  20)]
//        public string PaymentTypeSt { get; set; }

        public string ModeName { get; set; }

        public string InvName { get; set; }*/

    }
}