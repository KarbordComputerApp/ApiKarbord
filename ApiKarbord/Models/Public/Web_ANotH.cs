namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_ANotH
    {
        [Key]
        public int SerialNumber { get; set; }

        public int? DocNo { get; set; }

        public string DocDate { get; set; }

        public string Spec { get; set; }

        public string Tanzim { get; set; }

        public string Taeed { get; set; }

        public string TahieShode { get; set; }

        public string Mod { get; set; }

        public string Type { get; set; }

        public string TypeName { get; set; }

        public bool? JournalDone { get; set; }

        public string hDate { get; set; }

        public string hTime { get; set; }

        public DateTime? hDateTime { get; set; }

        public DateTime? mDocDate { get; set; }

        public string Eghdam { get; set; }

        public string JournalDate { get; set; }

        public bool? RelatedGroupActive { get; set; }

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

        public bool? ChangeNotify { get; set; }

        public string Taeed2 { get; set; }

        public string Taeed2Date { get; set; }

        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public short? Branch { get; set; }

        public int? RelatedGroup_FromGroup { get; set; }

        public long? RelatedGroup_FromSerialNumber { get; set; }

        public int? PrintCount { get; set; }

        public string PrintDate { get; set; }

        public byte? LegalRoozNoMode { get; set; }

        public int? LegalRoozNo { get; set; }

        public DateTime? EghdamDate { get; set; }

        public short? EghdamBranch { get; set; }

        public string Attach { get; set; }
    }
}