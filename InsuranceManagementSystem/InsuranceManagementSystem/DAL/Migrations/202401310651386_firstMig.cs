namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppliedPolicies", "PolicyType", c => c.Int(nullable: false));
            AddColumn("dbo.AppliedPolicies", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Policies", "PolicyType", c => c.Int(nullable: false));
            AddColumn("dbo.Policies", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Policies", "Price");
            DropColumn("dbo.Policies", "PolicyType");
            DropColumn("dbo.AppliedPolicies", "Price");
            DropColumn("dbo.AppliedPolicies", "PolicyType");
        }
    }
}
