namespace ApiKarbord.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Access")]
    public partial class Access
    {
        public int Id { get; set; }

        [Required]
        public string lockNumber { get; set; }

        public string CompanyName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string AddressApi { get; set; }

        public string SqlServerName { get; set; }

        public string SqlUserName { get; set; }

        public string SqlPassword { get; set; }

        public string fromDate { get; set; }

        public string toDate { get; set; }

        public byte? userCount { get; set; }
      
        public string AFI1_Group { get; set; }

        public string AFI1_Access { get; set; }

        public string AFI8_Group { get; set; }

        public string AFI8_Access { get; set; }

        public string ERJ_Group { get; set; }

        public string ERJ_Access { get; set; }

        public bool? active { get; set; }

        public bool? multilang { get; set; }

        public int? logoutmin { get; set; }

        public string AddressApiPos { get; set; }

        public bool? IsApp { get; set; }

        public bool? IsWeb { get; set; }

        public bool? IsApi { get; set; }

        public string WhereKala { get; set; }

        public string WhereCust { get; set; }

        public string WhereThvl { get; set; }

        public string WhereAcc { get; set; }

        public string Spec { get; set; }

        public string ProgName { get; set; }

        public string Fct_or_Inv { get; set; }

        public int? SettingApp { get; set; }

        public string ActiveSamane { get; set; }

        public string SamaneGroup { get; set; }

        public byte? SamaneProg { get; set; }

        public ApiModel DB { get; set; }

    }
}
