namespace ApiKarbord.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ApiModel : DbContext
    {


        public virtual DbSet<AccessSet> AccessSet { get; set; }
        
        public virtual DbSet<Web_RprtCols> Web_RprtCols { get; set; }

        public virtual DbSet<MojodiKala> MojodiKala { get; set; }
        public virtual DbSet<AFI_FDocBi> AFI_FDocBi { get; set; }
        public virtual DbSet<AFI_FDocHi> AFI_FDocHi { get; set; }
        public virtual DbSet<Web_Kala> Web_Kala { get; set; }
        public virtual DbSet<Web_Cust> Web_Cust { get; set; }

        public virtual DbSet<Web_CGru> Web_CGru { get; set; }

        public virtual DbSet<Web_Acc> Web_Acc { get; set; }
        public virtual DbSet<Web_CheckStatus> Web_CheckStatus { get; set; }
        public virtual DbSet<Web_KalaPrice> Web_KalaPrice { get; set; }
        public virtual DbSet<Web_FDocB> Web_FDocB { get; set; }
        public virtual DbSet<Web_Unit> Web_Unit { get; set; }
        public virtual DbSet<Web_Inv> Web_Inv { get; set; }
        public virtual DbSet<Web_KalaPriceB> Web_KalaPriceB { get; set; }
        public virtual DbSet<Web_Param> Web_Param { get; set; }
        public virtual DbSet<Web_AddMin> Web_AddMin { get; set; }
        public virtual DbSet<Web_FDocH> Web_FDocH { get; set; }
        public virtual DbSet<Web_FDocHMini> Web_FDocHMini { get; set; }
        public virtual DbSet<Web_Payment> Web_Payment { get; set; }
        public virtual DbSet<Web_Status> Web_Status { get; set; }
        public virtual DbSet<Web_Mkz> Web_Mkz { get; set; }
        public virtual DbSet<Web_Opr> Web_Opr { get; set; }
        public virtual DbSet<Web_KGru> Web_KGru { get; set; }

        public virtual DbSet<Web_AMode> Web_AMode { get; set; }
        public virtual DbSet<Web_FMode> Web_FMode { get; set; }
        public virtual DbSet<Web_IMode> Web_IMode { get; set; }
        public virtual DbSet<Web_ADocH> Web_ADocH { get; set; }
        public virtual DbSet<Web_ADocB> Web_ADocB { get; set; }
        public virtual DbSet<Web_ZAcc> Web_ZAcc { get; set; }
        public virtual DbSet<Web_CheckList> Web_CheckList { get; set; }


        // مدل های انبار مالی بازرگانی

        public virtual DbSet<AFI_IDocBi> AFI_IDocBi { get; set; }
        public virtual DbSet<AFI_IDocHi> AFI_IDocHi { get; set; }
        public virtual DbSet<Web_IDocB> Web_IDocB { get; set; }
        public virtual DbSet<Web_IDocH> Web_IDocH { get; set; }
        public virtual DbSet<Web_IDocHMini> Web_IDocHMini { get; set; }
        public virtual DbSet<Web_Thvl> Web_Thvl { get; set; }



        // مدل های گزارشات انبار مالی بازرگانی
        public virtual DbSet<Web_TrzIKala> Web_TrzIKala { get; set; }

        public virtual DbSet<Web_Dftr> Web_Dftr { get; set; }

        public virtual DbSet<Web_IDocR> Web_IDocR { get; set; }

        public virtual DbSet<Web_TrzFKala> Web_TrzFKala { get; set; }
        public virtual DbSet<Web_TrzFCust> Web_TrzFCust { get; set; }

        public virtual DbSet<Web_TChk> Web_TChk { get; set; }



        // مدل های سیستم اتوماسیون اداری

        // public virtual DbSet<Web_DocBDocCUser> Web_DocBDocCUser { get; set; }
        // public virtual DbSet<Web_ErjCust> Web_ErjCust { get; set; }
        // public virtual DbSet<Web_ErjDocK> Web_ErjDocK { get; set; }
        // public virtual DbSet<Web_Khdt> Web_Khdt { get; set; }  

        public ApiModel(string connectionString) : base(connectionString)
        {

            Database.SetInitializer<ApiModel>(null);
            // Database.SetInitializer<ApiModel>(new CreateDatabaseIfNotExists<ApiModel>());
            SetConnectionString(connectionString);

            //Configuration.ProxyCreationEnabled = false;
        }

        public void SetConnectionString(string connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
            // this.Database.Connection.;
        }
    }

}
