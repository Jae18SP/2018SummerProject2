namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableMaster : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TableMasters",
                c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        TableType = c.String(nullable: false),
                        TabeSize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TableId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TableMasters");
        }
    }
}
