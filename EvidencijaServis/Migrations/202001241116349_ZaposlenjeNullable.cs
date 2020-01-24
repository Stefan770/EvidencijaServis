namespace EvidencijaServis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZaposlenjeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zaposlenis", "GodinaZaposlenja", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaposlenis", "GodinaZaposlenja", c => c.Int(nullable: false));
        }
    }
}
