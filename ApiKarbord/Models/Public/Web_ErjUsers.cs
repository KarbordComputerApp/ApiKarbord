namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_ErjUsers
    {
        public string Code { get; set; }
         
        public int? GroupNo { get; set; }

        public string Trs { get; set; }

        public string ProgName { get; set; }

        public string Name { get; set; }

        public string Psw { get; set; }

        //public image Emza { get; set; }

        public byte Version { get; set; }

        public string LtnName { get; set; }

        public string TrsRprt { get; set; }

        public string Spec { get; set; }

        public string VstrCode { get; set; } 
    }
}