namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Web_RprtCols
    {


       
       
        public string RprtId { get; set; }

      
        public string UserCode { get; set; }

        [Key]
        public string Code { get; set; }

        public int Type { get; set; }

        public byte? Visible { get; set; }

        public string Prog { get; set; }
       
        public string Name { get; set; }
      
        /*public string RprtId { get; set; }

        public string UserCode { get; set; }

        [Key]

        public string Code { get; set; }


        public byte Type { get; set; }

        public byte? Visible { get; set; }


        public string Prog { get; set; }


        public string Name { get; set; }*/

    }
}